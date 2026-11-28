-- LAB 1
CREATE DATABASE IF NOT EXISTS DB_Dorado;
USE DB_Dorado;

-- DROP TABLE IF EXISTS Students;
CREATE TABLE IF NOT EXISTS Students(
  student_id INT PRIMARY KEY AUTO_INCREMENT,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  date_of_birth DATE NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
);

-- DROP TABLE IF EXISTS Courses;
CREATE TABLE IF NOT EXISTS Courses(
  course_id INT PRIMARY KEY AUTO_INCREMENT,
  course_name VARCHAR(100) NOT NULL,
  course_code VARCHAR(20) NOT NULL UNIQUE
);

-- LAB 2
USE DB_Dorado;

DROP TABLE IF EXISTS Instructors;
CREATE TABLE IF NOT EXISTS Instructors(
  instructor_id INT PRIMARY KEY AUTO_INCREMENT,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  department VARCHAR(50)
);

ALTER TABLE Courses
ADD COLUMN IF NOT EXISTS instructor_id INT;

ALTER TABLE Courses
ADD FOREIGN KEY (instructor_id) REFERENCES Instructors(instructor_id);

DROP TABLE IF EXISTS Enrollments;
CREATE TABLE IF NOT EXISTS Enrollments(
  enrollment_id INT PRIMARY KEY AUTO_INCREMENT,
  student_id INT NOT NULL,
  course_id INT NOT NULL,
  enrollment_date DATE NOT NULL,
  FOREIGN KEY (student_id) REFERENCES Students(student_id),
  FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

-- LAB 3
USE DB_Dorado;

DROP TABLE IF EXISTS Roles;
CREATE TABLE IF NOT EXISTS Roles(
  role_id INT PRIMARY KEY AUTO_INCREMENT,
  role_name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO Roles (role_name) VALUES ('Admin'), ('Teacher'), ('Student');

DROP TABLE IF EXISTS Users;
CREATE TABLE IF NOT EXISTS Users(
  user_id INT PRIMARY KEY AUTO_INCREMENT,
  username VARCHAR(255) NOT NULL UNIQUE,
  password_hash VARCHAR(255) NOT NULL,
  role_id INT NOT NULL,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

ALTER TABLE Instructors
  ADD COLUMN user_id INT UNIQUE;

ALTER TABLE Instructors
  ADD CONSTRAINT fk_instructors_user FOREIGN KEY (user_id) REFERENCES Users(user_id);
ALTER TABLE Instructors
  MODIFY user_id INT NOT NULL;

ALTER TABLE Students
  ADD COLUMN user_id INT UNIQUE;
ALTER TABLE Students
  ADD CONSTRAINT fk_students_user FOREIGN KEY (user_id) REFERENCES Users(user_id);
ALTER TABLE Students
  MODIFY user_id INT NOT NULL;

ALTER TABLE Enrollments
  ADD CONSTRAINT unique_enrollment UNIQUE(student_id, course_id);
  
USE DB_Dorado;
INSERT INTO Users (username, password_hash, role_id)
VALUES ('admin', '$2a$12$65Zu4MqX3J0P2u2tlGtO4OPILSZTl9A0BxHcaGUDcfzgGFdayLl5y', 1);

-- LAB 4

USE DB_Dorado;

DROP TABLE IF EXISTS AssignmentTypes;
CREATE TABLE IF NOT EXISTS AssignmentTypes(
    type_id INT PRIMARY KEY AUTO_INCREMENT,
    type_name VARCHAR(100) UNIQUE NOT NULL,
    default_weight DECIMAL(5,2) NOT NULL
);
DESCRIBE AssignmentTypes;

INSERT INTO AssignmentTypes (type_name, default_weight) 
VALUES
('Quiz', 10.00),
('Assignment', 20.00),
('Midterm Exam', 30.00),
('Final Exam', 40.00);
 
DROP TABLE IF EXISTS Grades;

CREATE TABLE IF NOT EXISTS Grades(
    student_id INT NOT NULL,
    course_id INT NOT NULL,
    type_id INT NOT NULL,
    grade DECIMAL(5,2) NOT NULL,
    date_recorded DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (student_id, course_id, type_id),
    FOREIGN KEY (student_id) REFERENCES Students(student_id),
    FOREIGN KEY (course_id) REFERENCES Courses(course_id),
    FOREIGN KEY (type_id) REFERENCES AssignmentTypes(type_id),
    CONSTRAINT chk_grade_range CHECK (grade >= 0 AND grade <= 100)
);
DESCRIBE Grades;
DROP VIEW IF EXIST StudentGradeSummary;

CREATE VIEW IF NOT EXISTS StudentGradeSummary AS
SELECT
    s.user_id,
    stu.student_id,
    c.course_code,
    c.course_name,
    at.type_name,
    g.grade,
    g.date_recorded
FROM Grades g
INNER JOIN Students stu ON g.student_id = stu.student_id
INNER JOIN Users s ON stu.user_id = s.user_id
INNER JOIN Courses c ON g.course_id = c.course_id
INNER JOIN AssignmentTypes at ON g.type_id = at.type_id;

DESCRIBE StudentGradeSummary;

DROP VIEW IF EXISTS TeacherCourseOverview;

CREATE VIEW IF NOT EXISTS TeacherCourseOverview AS 
SELECT
    i.user_id,
    c.course_id,
    c.course_code,
    c.course_name,
    at.type_name,
    COUNT(g.grade) AS submissions,
    AVG(g.grade) AS average_grade
FROM Courses c
INNER JOIN Instructors i ON c.instructor_id = i.instructor_id
LEFT JOIN Grades g ON c.course_id = g.course_id
LEFT JOIN AssignmentTypes at ON g.type_id = at.type_id
GROUP BY c.course_id, at.type_id, i.user_id;



DELETE FROM students
WHERE first_name  = "Mark Lawrence";


DESCRIBE TeacherCourseOverview;

USE DB_Dorado;

-- LAB 5
CREATE INDEX IF NOT EXISTS idx_grades_student_course
ON Grades(student_id, course_id);

CREATE INDEX IF NOT EXISTS idx_enrollments_date
ON Enrollments(enrollment_date);

CREATE INDEX IF NOT EXISTS idx_courses_search
ON Courses(course_code, course_name);

EXPLAIN SELECT * FROM Grades WHERE student_id = 1 AND course_id = 101;
EXPLAIN SELECT * FROM Enrollments WHERE enrollment_date = '2025-01-15';
EXPLAIN SELECT * FROM Courses WHERE course_code = 'IT101';

DROP PROCEDURE IF EXISTS sp_EnrollStudent;

DELIMITER $$
CREATE PROCEDURE sp_EnrollStudent(
    IN p_student_id INT,
    IN p_course_id INT,
    OUT p_result VARCHAR(255)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_result = 'Error: Enrollment failed';
    END;

    START TRANSACTION;

    IF NOT EXISTS (SELECT 1 FROM Students WHERE student_id = p_student_id) THEN
        SET p_result = 'Error: Student not found';
        ROLLBACK;
    ELSEIF NOT EXISTS (SELECT 1 FROM Courses WHERE course_id = p_course_id) THEN
        SET p_result = 'Error: Course not found';
        ROLLBACK;
    ELSEIF EXISTS (SELECT 1 FROM Enrollments WHERE student_id = p_student_id AND course_id = p_course_id) THEN
        SET p_result = 'Error: Student already enrolled in this course';
        ROLLBACK;
    ELSE
        INSERT INTO Enrollments (student_id, course_id, enrollment_date)
        VALUES (p_student_id, p_course_id, NOW());

        SET p_result = 'Success: Student enrolled successfully';
        COMMIT;
    END IF;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CalculateCourseGrade;
DELIMITER $$
CREATE PROCEDURE sp_CalculateCourseGrade(
    IN p_student_id INT,
    IN p_course_id INT,
    OUT p_final_grade DECIMAL(5,2)
)
BEGIN
    SELECT SUM(g.grade * at.default_weight / 100) INTO p_final_grade
    FROM Grades g
    INNER JOIN AssignmentTypes at ON g.type_id = at.type_id
    WHERE g.student_id = p_student_id
    AND g.course_id = p_course_id;
END $$
DELIMITER ;

-- 1. Enroll a student who is NOT yet in that course
CALL sp_EnrollStudent(1, 101, @enroll_status);
SELECT @enroll_status AS Enrollment_Result;

-- 2. Run it again to show your duplicate-check validation works!
CALL sp_EnrollStudent(1, 101, @enroll_status_duplicate);
SELECT @enroll_status_duplicate AS Duplicate_Test_Result;


DROP VIEW IF EXISTS StudentTranscript;
CREATE VIEW StudentTranscript AS
SELECT
    s.student_id,
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    c.course_code,
    c.course_name,
    at.type_name AS assignment_type,
    g.grade,
    at.default_weight,
    e.enrollment_date,
    i.first_name AS instructor_first_name,
    i.last_name AS instructor_last_name
FROM Students s
INNER JOIN Grades g ON s.student_id = g.student_id
INNER JOIN Courses c ON g.course_id = c.course_id
INNER JOIN AssignmentTypes at ON g.type_id = at.type_id
INNER JOIN Enrollments e ON s.student_id = e.student_id AND c.course_id = e.course_id
LEFT JOIN Instructors i ON c.instructor_id = i.instructor_id;

DROP VIEW IF EXISTS CoursePerformanceAnalytics;
CREATE VIEW CoursePerformanceAnalytics AS
SELECT
    c.course_id,
    c.course_code,
    c.course_name,
    COUNT(DISTINCT e.student_id) AS enrolled_students,
    COUNT(g.grade) AS grades_submitted,
    AVG(g.grade) AS average_grade,
    MIN(g.grade) AS minimum_grade,
    MAX(g.grade) AS maximum_grade,
    CONCAT(i.first_name, ' ', i.last_name) AS instructor_name
FROM Courses c
LEFT JOIN Enrollments e ON c.course_id = e.course_id
LEFT JOIN Grades g ON c.course_id = g.course_id
LEFT JOIN Instructors i ON c.instructor_id = i.instructor_id
GROUP BY c.course_id, c.course_code, c.course_name, instructor_name;

-- 
CREATE TABLE IF NOT EXISTS FinalGrades (
    student_id INT NOT NULL,
    course_id INT NOT NULL,
    calculated_grade DECIMAL(5,2),
    final_grade DECIMAL(5,2) NOT NULL,
    is_overridden BOOLEAN NOT NULL DEFAULT FALSE,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (student_id, course_id),
    FOREIGN KEY (student_id) REFERENCES Students(student_id),
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

SELECT i.instructor_id, i.user_id, u.username, c.course_id, c.course_code, c.course_name
FROM Instructors i
JOIN Users u ON i.user_id = u.user_id
LEFT JOIN Courses c ON c.instructor_id = i.instructor_id;

SELECT e.enrollment_id, e.student_id, s.first_name, s.last_name,
       e.course_id, c.course_code, c.course_name, e.enrollment_date
FROM Enrollments e
JOIN Students s ON e.student_id = s.student_id
JOIN Courses c ON e.course_id = c.course_id;

SELECT * FROM TeacherCourseOverview;

SELECT * FROM Enrollments;

USE DB_Dorado;

-- LAB 6
CREATE TABLE IF NOT EXISTS AuditLog (
    audit_id          INT PRIMARY KEY AUTO_INCREMENT,
    table_name        VARCHAR(100) NOT NULL,
    record_id         INT NOT NULL,
    action_type       ENUM('INSERT', 'UPDATE', 'DELETE') NOT NULL,
    old_values        JSON,
    new_values        JSON,
    user_id           INT NOT NULL,
    action_timestamp  DATETIME DEFAULT CURRENT_TIMESTAMP,
    ip_address        VARCHAR(45),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE IF NOT EXISTS BackupLog (
    backup_id         INT PRIMARY KEY AUTO_INCREMENT,
    backup_timestamp  VARCHAR(20) NOT NULL,
    description       VARCHAR(255),
    status            VARCHAR(20) NOT NULL DEFAULT 'STARTED',
    created_at        DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS UserSessions (
    session_id     INT PRIMARY KEY AUTO_INCREMENT,
    user_id        INT NOT NULL,
    login_time     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_activity  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    logout_time    DATETIME NULL,
    is_active      TINYINT(1) NOT NULL DEFAULT 1,
    ip_address     VARCHAR(45),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE USER IF NOT EXISTS 'academic_admin'@'localhost', 'academic_admin'@'127.0.0.1' IDENTIFIED BY 'secure_password_123';
CREATE USER IF NOT EXISTS 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1' IDENTIFIED BY 'secure_password_456';
CREATE USER IF NOT EXISTS 'academic_student'@'localhost', 'academic_student'@'127.0.0.1' IDENTIFIED BY 'secure_password_789';
CREATE USER IF NOT EXISTS 'academic_login'@'localhost', 'academic_login'@'127.0.0.1' IDENTIFIED BY 'secure_password_000';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON DB_Dorado.* TO 'academic_admin'@'localhost', 'academic_admin'@'127.0.0.1';

GRANT SELECT, INSERT, UPDATE ON DB_Dorado.Grades TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.StudentGradeSummary TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.TeacherCourseOverview TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.StudentTranscript TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.CoursePerformanceAnalytics TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.Courses TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.Instructors TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.Students TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.Enrollments TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.AssignmentTypes TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';
GRANT EXECUTE ON PROCEDURE DB_Dorado.sp_CalculateCourseGrade TO 'academic_teacher'@'localhost', 'academic_teacher'@'127.0.0.1';

GRANT SELECT ON DB_Dorado.StudentGradeSummary TO 'academic_student'@'localhost', 'academic_student'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.StudentTranscript TO 'academic_student'@'localhost', 'academic_student'@'127.0.0.1';
GRANT SELECT ON DB_Dorado.Courses TO 'academic_student'@'localhost', 'academic_student'@'127.0.0.1';

GRANT SELECT ON DB_Dorado.Users TO 'academic_login'@'localhost', 'academic_login'@'127.0.0.1';
GRANT SELECT, INSERT, UPDATE ON DB_Dorado.UserSessions TO 'academic_login'@'localhost', 'academic_login'@'127.0.0.1';

FLUSH PRIVILEGES;

ALTER USER 'academic_login'@'localhost' IDENTIFIED BY 'secure_password_000';
ALTER USER 'academic_login'@'127.0.0.1' IDENTIFIED BY 'secure_password_000';
FLUSH PRIVILEGES;

DROP TRIGGER IF EXISTS tr_grades_audit_insert;
DROP TRIGGER IF EXISTS tr_grades_audit_update;
DROP TRIGGER IF EXISTS tr_validate_grade_before_insert;

DELIMITER //

CREATE TRIGGER tr_grades_audit_insert
AFTER INSERT ON Grades
FOR EACH ROW
BEGIN
    INSERT INTO AuditLog (table_name, record_id, action_type, new_values, user_id, ip_address)
    VALUES ('Grades', NEW.student_id, 'INSERT',
            JSON_OBJECT('student_id', NEW.student_id, 'course_id', NEW.course_id,
                        'type_id', NEW.type_id, 'grade', NEW.grade),
            @current_user_id, @current_user_ip);
END //

CREATE TRIGGER tr_grades_audit_update
AFTER UPDATE ON Grades
FOR EACH ROW
BEGIN
    INSERT INTO AuditLog (table_name, record_id, action_type,
                          old_values, new_values, user_id, ip_address)
    VALUES ('Grades', NEW.student_id, 'UPDATE',
            JSON_OBJECT('student_id', OLD.student_id, 'course_id', OLD.course_id,
                        'type_id', OLD.type_id, 'grade', OLD.grade),
            JSON_OBJECT('student_id', NEW.student_id, 'course_id', NEW.course_id,
                        'type_id', NEW.type_id, 'grade', NEW.grade),
            @current_user_id, @current_user_ip);
END //

CREATE TRIGGER tr_validate_grade_before_insert
BEFORE INSERT ON Grades
FOR EACH ROW
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM Enrollments
        WHERE student_id = NEW.student_id AND course_id = NEW.course_id
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot add grade: Student is not enrolled in this course';
    END IF;

    IF EXISTS (
        SELECT 1 FROM Grades
        WHERE student_id = NEW.student_id
        AND course_id = NEW.course_id
        AND type_id = NEW.type_id
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Duplicate grade entry: Student already has a grade for this assignment type';
    END IF;
END //

DROP PROCEDURE IF EXISTS sp_CreateDatabaseBackup //
CREATE PROCEDURE sp_CreateDatabaseBackup(IN backup_description VARCHAR(255))
BEGIN
    DECLARE v_timestamp VARCHAR(20);
    SET v_timestamp = DATE_FORMAT(NOW(), '%Y%m%d_%H%i%s');

    INSERT INTO BackupLog (backup_timestamp, description, status)
    VALUES (v_timestamp, backup_description, 'STARTED');

    UPDATE BackupLog
    SET status = 'COMPLETED'
    WHERE backup_id = LAST_INSERT_ID();
END //

DELIMITER ;


USE DB_Dorado;

-- Optional: per-course fee (default 0.00 so existing courses don't break)
ALTER TABLE Courses
    ADD COLUMN IF NOT EXISTS course_fee DECIMAL(10,2) NOT NULL DEFAULT 0.00;

-- Payments table
CREATE TABLE IF NOT EXISTS Payments (
    payment_id      INT PRIMARY KEY AUTO_INCREMENT,
    student_id      INT NOT NULL,
    course_id       INT NULL,
    amount_paid     DECIMAL(10,2) NOT NULL,
    payment_method  VARCHAR(50) NOT NULL DEFAULT 'Cash',
    reference_no    VARCHAR(100) NULL,
    payment_date    DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    remarks         VARCHAR(255) NULL,
    CONSTRAINT fk_payments_student FOREIGN KEY (student_id) REFERENCES Students(student_id),
    CONSTRAINT fk_payments_course FOREIGN KEY (course_id) REFERENCES Courses(course_id),
    CONSTRAINT chk_payment_positive CHECK (amount_paid > 0)
);

-- Statement of Account (SOA) summary per student
DROP VIEW IF EXISTS StudentSOA;
CREATE VIEW StudentSOA AS
SELECT
    s.student_id,
    CONCAT(s.first_name, ' ', s.last_name) AS student_name,
    s.email,
    COUNT(DISTINCT e.course_id) AS enrolled_courses,
    COALESCE(fees.total_assessed, 0.00) AS total_assessed,
    COALESCE(paid.total_paid, 0.00) AS total_paid,
    COALESCE(fees.total_assessed, 0.00) - COALESCE(paid.total_paid, 0.00) AS balance_due,
    CASE
        WHEN COALESCE(fees.total_assessed, 0.00) - COALESCE(paid.total_paid, 0.00) <= 0
             AND COALESCE(fees.total_assessed, 0.00) > 0 THEN 'PAID'
        WHEN COALESCE(paid.total_paid, 0.00) > 0 THEN 'PARTIAL'
        ELSE 'UNPAID'
    END AS payment_status
FROM Students s
LEFT JOIN Enrollments e ON s.student_id = e.student_id
LEFT JOIN (
    SELECT
        e2.student_id,
        SUM(COALESCE(c.course_fee, 0.00)) AS total_assessed
    FROM Enrollments e2
    INNER JOIN Courses c ON e2.course_id = c.course_id
    GROUP BY e2.student_id
) fees ON s.student_id = fees.student_id
LEFT JOIN (
    SELECT
        student_id,
        SUM(amount_paid) AS total_paid
    FROM Payments
    GROUP BY student_id
) paid ON s.student_id = paid.student_id
GROUP BY
    s.student_id,
    s.first_name,
    s.last_name,
    s.email,
    fees.total_assessed,
    paid.total_paid;