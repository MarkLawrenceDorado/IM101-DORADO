Public Class UndoManager

    Private Class UndoStep
        Public Property Description As String
        Public Property UndoAction As Action
        Public Property RedoAction As Action
    End Class

    Private ReadOnly _undoStack As New Stack(Of UndoStep)()
    Private ReadOnly _redoStack As New Stack(Of UndoStep)()

    Public Event Changed()

    Public ReadOnly Property CanUndo As Boolean
        Get
            Return _undoStack.Count > 0
        End Get
    End Property

    Public ReadOnly Property CanRedo As Boolean
        Get
            Return _redoStack.Count > 0
        End Get
    End Property

    Public ReadOnly Property NextUndoText As String
        Get
            Return If(CanUndo, _undoStack.Peek().Description, "")
        End Get
    End Property

    Public ReadOnly Property NextRedoText As String
        Get
            Return If(CanRedo, _redoStack.Peek().Description, "")
        End Get
    End Property

    ' Call after a change has been made. A new change clears everything that could be redone.
    Public Sub Record(description As String, undoAction As Action, redoAction As Action)
        Dim item As New UndoStep()
        item.Description = description
        item.UndoAction = undoAction
        item.RedoAction = redoAction

        _undoStack.Push(item)
        _redoStack.Clear()
        RaiseEvent Changed()
    End Sub

    Public Sub Undo()
        If Not CanUndo Then Return

        Dim item As UndoStep = _undoStack.Pop()
        item.UndoAction?.Invoke()
        _redoStack.Push(item)
        RaiseEvent Changed()
    End Sub

    Public Sub Redo()
        If Not CanRedo Then Return

        Dim item As UndoStep = _redoStack.Pop()
        item.RedoAction?.Invoke()
        _undoStack.Push(item)
        RaiseEvent Changed()
    End Sub

    Public Sub Clear()
        _undoStack.Clear()
        _redoStack.Clear()
        RaiseEvent Changed()
    End Sub

End Class