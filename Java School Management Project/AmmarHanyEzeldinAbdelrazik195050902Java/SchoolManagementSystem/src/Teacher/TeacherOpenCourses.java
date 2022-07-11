package Teacher;

import Database.DbConnection;
import Publics.PublicFunctions;
import Publics.PublicVariables;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherOpenCourses {
    private JPanel mainPanel;
    private JButton button_Back;
    private JButton button_Close;
    private JButton button_Open;
    private JTable table_Courses;

    public TeacherOpenCourses() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Courses.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Courses.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString();

            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });

        button_Close.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Select * from Open_Courses where CourseID like ?";
                    Connection conn = DbConnection.ConnectionDB();
                    PreparedStatement pst = conn.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString()));
                    ResultSet rs = pst.executeQuery();
                    if(!rs.next()){
                        PublicFunctions.infoBox("This Course is already Closed", "Info");
                        return;
                    }
                    pst.close();
                    conn.close();

                } catch (Exception x) {
                    PublicFunctions.infoBox("Error hena" + x.toString(), "Error at Courses Selection");
                }
                try {
                    String query = "Delete from Open_Courses where CourseID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }
                TeacherOpenCourses win = new TeacherOpenCourses();
                frame.dispose();
            }
        });
        button_Open.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Select * from Open_Courses where CourseID like ?";
                    Connection conn = DbConnection.ConnectionDB();
                    PreparedStatement pst = conn.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString()));
                    ResultSet rs = pst.executeQuery();
                    while (rs.next()) {
                        if (rs.getInt(2) == Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString())) {
                            PublicFunctions.infoBox("This Course is already Opened", "Info");
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
                    String query = "Insert into Open_Courses (CourseID) values (?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }
                TeacherOpenCourses win = new TeacherOpenCourses();
                frame.dispose();
            }
        });
    }

    void fillData() {

        try {
            String query = "select Courses.ID, Courses.Name , Courses.Semester , Courses.Credit from Courses where TeacherID like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            pst.setInt(1, PublicVariables.teacher.getID());
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Courses);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}