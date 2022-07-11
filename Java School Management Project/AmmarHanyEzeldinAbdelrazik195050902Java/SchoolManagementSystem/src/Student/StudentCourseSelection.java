package Student;

import Database.DbConnection;
import Publics.PublicFunctions;
import Publics.PublicVariables;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class StudentCourseSelection {
    private JPanel mainPanel;
    private JTable table_CanEnroll;
    private JTable table_Enrolled;
    private JButton button_Add;
    private JButton button_Remove;
    private JButton button_Back;
    private JButton button_Save;
    private JTextField text_CanEnrollSearch;
    private JLabel label_CanEnrollSearch;
    private JLabel label_EnrolledSearch;
    private JTextField text_EnrolledSearch;

    public StudentCourseSelection() {

        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);
        fillTable();
        fillTable2();
        frame.setVisible(true);


        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int courseID = -1;
                try {
                    courseID = Integer.parseInt(table_CanEnroll.getValueAt(table_CanEnroll.getSelectedRow(), 0).toString());
                } catch (Exception x) {
                    PublicFunctions.infoBox("Please Select a course to ADD! ", "Select a course");
                    return;
                }
                DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
                LocalDateTime now = LocalDateTime.now();
                try {
                    String query = "Select * from Grades where CourseID like ? and IsFinished like 0";
                    Connection conn = DbConnection.ConnectionDB();
                    PreparedStatement pst = conn.prepareStatement(query);
                    pst.setInt(1, courseID);
                    ResultSet rs = pst.executeQuery();
                    while (rs.next()){
                        if (rs.getInt(11) == 1){
                            PublicFunctions.infoBox("This Course has already been taken and agreed","Info");
                            pst.close();
                            conn.close();
                            return;
                        } if (rs.getInt(10) == 1){
                            PublicFunctions.infoBox("This Course has already been taken","Info");
                            pst.close();
                            conn.close();
                            return;
                        }
                    }
                    pst.close();
                    conn.close();


                } catch (Exception x) {
                    PublicFunctions.infoBox("Error hena" + x.toString(), "Error at Courses Selection");
                }
                try {
                    String query = "INSERT INTO Grades (StudentID,CourseID,IsFinished,IsPassed,CourseDate,IsTaken,IsAgreed) VALUES(?,?,?,?,?,?,?)";
                    Connection conn = DbConnection.ConnectionDB();
                    PreparedStatement pst = conn.prepareStatement(query);
                    pst.setInt(1, PublicVariables.student.getID());
                    pst.setInt(2, courseID);
                    pst.setInt(3, 0);
                    pst.setInt(4, 0);
                    pst.setString(5, now.toString());
                    pst.setInt(6, 1);
                    pst.setInt(7, 0);
                    pst.executeUpdate();
                    pst.close();
                    conn.close();


                } catch (Exception x) {
                    PublicFunctions.infoBox("Error hena" + x.toString(), "Error at Courses Selection");
                }

                fillTable();
                fillTable2();


            }
        });


        button_Remove.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int courseID = -1;
                try {
                    courseID = Integer.parseInt(table_Enrolled.getValueAt(table_Enrolled.getSelectedRow(), 0).toString());
                } catch (Exception x) {
                    PublicFunctions.infoBox("Please Select The Course to Remove! ", "Select a course");
                    return;
                }
                DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
                LocalDateTime now = LocalDateTime.now();
                try {
                    String query = "DELETE FROM Grades WHERE CourseID like ? and StudentID like ? and IsFinished = 0 and IsPassed = 0 And IsAgreed = 0 And IsTaken = 1";
                    Connection conn = DbConnection.ConnectionDB();
                    PreparedStatement pst = conn.prepareStatement(query);
                    pst.setInt(1, courseID);
                    pst.setInt(2, PublicVariables.student.getID());
                    pst.executeUpdate();
                    pst.close();
                    conn.close();


                } catch (Exception x) {
                    PublicFunctions.infoBox("Error hena" + x.toString(), "Error at Courses Selection");
                }

                fillTable();
                fillTable2();


            }
        });


        button_Save.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentMainMenu win = new StudentMainMenu();
                frame.dispose();
            }
        });
    }

    void fillTable() {
        try {

            Connection conn = DbConnection.ConnectionDB();
            String query = "Select Open_Courses.CourseID,Courses.Name , Courses.Credit , Courses.Semester from Open_Courses Left Join Courses ON Open_Courses.CourseID = Courses.ID where Courses.DepartmentID = ?";
            PreparedStatement pst = conn.prepareStatement(query);
            pst.setInt(1, PublicVariables.student.getDepartmentID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_CanEnroll);
            rs.close();
            conn.close();
        } catch (Exception x) {
            PublicFunctions.infoBox("Failed \n" + x, "Failed At Courses");
            return;
        }
    }

    void fillTable2() {
        Connection conn = DbConnection.ConnectionDB();
        try {


            String query = "Select  Grades.CourseID , Courses.Name , Courses.Credit , Courses.Semester from Grades Left Join Courses ON Grades.CourseID = Courses.ID where Grades.StudentID = ? And  Grades.IsAgreed = 0 And Grades.isFinished = 0 And Grades.IsPassed = 0";
            PreparedStatement pst = conn.prepareStatement(query);
            pst.setInt(1, PublicVariables.student.getDepartmentID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Enrolled);
            rs.close();
            conn.close();
        } catch (Exception x) {
            PublicFunctions.infoBox("Failed \n" + x, "Failed At Courses");
            return;
        }
    }
}
