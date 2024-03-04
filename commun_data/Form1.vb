Imports System.Data.Odbc
Public Class Form1
    Public conn As OdbcConnection
    Public dr As OdbcDataReader
    Public da As OdbcDataAdapter
    Public ds As DataSet
    Public cmd As OdbcCommand
    Public sql1 As String
    Public sql2 As String

    Sub koneksi()
        sql1 = "Driver={MySQL ODBC 5.1 Driver};server=localhost;uid=root;database=db_commun;port=3306"
        conn = New OdbcConnection(sql1)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

    End Sub

    Sub tampildgv()
        koneksi()
        da = New OdbcDataAdapter("Select * From tbl_siswa", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_siswa")
        Me.DGV.DataSource = (ds.Tables("tbl_siswa"))
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampildgv()
    End Sub

    Sub kosongkan()
        T1.Clear()
        T2.Clear()
        T3.Clear()
        T4.Clear()
    End Sub
    Sub aktifkan()
        T1.Enabled = True
        T2.Enabled = True
        T3.Enabled = True
        T4.Enabled = True
    End Sub
    Sub noaktif()
        T1.Enabled = False
        T2.Enabled = False
        T3.Enabled = False
        T4.Enabled = False
    End Sub
    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick
        koneksi()
        kosongkan()
        noaktif()
        tampildgv()
    End Sub

    Private Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click
        aktifkan()
    End Sub

    Private Sub B5_Click(sender As Object, e As EventArgs) Handles B5.Click
        Close()
    End Sub

    Private Sub B2_Click(sender As Object, e As EventArgs) Handles B2.Click
        koneksi()
        sql1 = "Insert Into tbl_siswa () values (" +
            "'" & T1.Text & "', " +
            "'" & T2.Text & "', " +
            "'" & T3.Text & "', " +
            "'" & T4.Text & "', " +
            "'" & T5.Text & "')"
        cmd = New OdbcCommand(sql1, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        DGV.Refresh()

        tampildgv()
        kosongkan()
        noaktif()
    End Sub

    Private Sub T2_TextChanged(sender As Object, e As EventArgs) Handles T2.TextChanged

    End Sub

    Private Sub B3_Click(sender As Object, e As EventArgs) Handles B3.Click
        If MessageBox.Show("Apakah anda ingin mengubah data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Call koneksi()
            sql1 = "Update pasien set nama = '" & T1.Text & "', umur = '" & T2.Text & "', alamat = '" & T3.Text & "', telepon = '" & T4.Text & "' where id_pasien = '" & T5.Text & "'"
            cmd = New OdbcCommand(sql1, conn)

            Try
                cmd.ExecuteNonQuery()
                DGV.Refresh()
                Call tampildgv()
                Call kosongkan()
                Call noaktif()
                MsgBox("Edit data berhasil")

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub B4_Click(sender As Object, e As EventArgs) Handles B4.Click
        If MessageBox.Show("Apakah anda ingin menghapus data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Call koneksi()
            sql1 = "delete from pasien where id_pasien ='" & T1.Text & "';"
            cmd = New OdbcCommand(sql1, conn)

            Try
                cmd.ExecuteNonQuery()
                DGV.Refresh()
                Call tampildgv()
                Call kosongkan()
                Call noaktif()
                MsgBox("Hapus data berhasil")

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class