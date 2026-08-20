-- LAB 1
CREATE DATABASE IF NOT EXISTS DB_Dorado;
USE DB_Dorado;
DROP TABLE IF EXISTS Students;
SET FOREIGN_KEY_CHECKS = 0;
CREATE TABLE IF NOT EXISTS Students(
  student_id INT PRIMARY KEY AUTO_INCREMENT,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  date_of_birth DATE NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
);

DROP TABLE IF EXISTS Courses;
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

SET FOREIGN_KEY_CHECKS = 1;

-- LAB 3 (corrected)
USE DB_Dorado;

-- 1. Roles FIRST (Users depends on it)
DROP TABLE IF EXISTS Roles;
CREATE TABLE IF NOT EXISTS Roles(
  role_id INT PRIMARY KEY AUTO_INCREMENT,
  role_name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO Roles (role_name) VALUES ('Admin'), ('Teacher'), ('Student');

-- 2. Users (now Roles exists)
DROP TABLE IF EXISTS Users;
CREATE TABLE IF NOT EXISTS Users(
  user_id INT PRIMARY KEY AUTO_INCREMENT,
  username VARCHAR(255) NOT NULL UNIQUE,
  password_hash VARCHAR(255) NOT NULL,
  role_id INT NOT NULL,
  FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

-- 3. Add user_id to the EXISTING Instructors table
ALTER TABLE Instructors
  ADD COLUMN user_id INT UNIQUE;
-- (leave nullable initially so it doesn't fail against any existing rows,
--  then tighten to NOT NULL once every row has a user_id)
ALTER TABLE Instructors
  ADD CONSTRAINT fk_instructors_user FOREIGN KEY (user_id) REFERENCES Users(user_id);
ALTER TABLE Instructors
  MODIFY user_id INT NOT NULL;

-- 4. Add user_id to the EXISTING Students table
ALTER TABLE Students
  ADD COLUMN user_id INT UNIQUE;
ALTER TABLE Students
  ADD CONSTRAINT fk_students_user FOREIGN KEY (user_id) REFERENCES Users(user_id);
ALTER TABLE Students
  MODIFY user_id INT NOT NULL;

-- 5. Enrollments unique constraint (this part of your script was correct)
ALTER TABLE Enrollments
  ADD CONSTRAINT unique_enrollment UNIQUE(student_id, course_id);
  
USE DB_Dorado;
INSERT INTO Users (username, password_hash, role_id)
VALUES ('admin', '$2a$12$65Zu4MqX3J0P2u2tlGtO4OPILSZTl9A0BxHcaGUDcfzgGFdayLl5y', 1);