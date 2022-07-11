package Teacher;

import Database.DbConnection;
import Publics.ComboboxItem;
import Publics.PublicFunctions;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherCourseManagement {
    private JPanel mainPanel;
    private JButton backButton;
    private JButton updateButton;
    private JButton deleteButton;
    private JTextField text_Name;
    private JComboBox combo_Department;
    private JTextField text_Semester;
    private JTextField text_Credit;
    private JComboBox combo_Teacher;
    private JButton button_Add;
    private JScrollPane scrollPane;

    private JTable table_Courses;

    public TeacherCourseManagement() {
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

        backButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                String name = text_Name.getText();
                String semester = text_Semester.getText();
                String credit = text_Credit.getText();
                int departmentID = ((ComboboxItem)combo_Department.getSelectedItem()).getId();
                int teacherID = ((ComboboxItem)combo_Teacher.getSelectedItem()).getId();


                try {
                    String query = "Insert Into Courses (name,Semester,Credit,DepartmentID,TeacherID) values (?,?,?,?,?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, name);
                    pst.setString(2, semester);
                    pst.setString(3, credit);
                    pst.setInt(4, departmentID);
                    pst.setInt(5, teacherID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }
                TeacherCourseManagement win = new TeacherCourseManagement();
                frame.dispose();

            }
        });

        table_Courses.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString();
               text_Name.setText(table_Courses.getValueAt(table_Courses.getSelectedRow(), 1).toString());
               text_Semester.setText(table_Courses.getValueAt(table_Courses.getSelectedRow(), 2).toString());
               text_Credit.setText(table_Courses.getValueAt(table_Courses.getSelectedRow(), 3).toString());
               combo_Department.getModel().setSelectedItem(new ComboboxItem(Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 4).toString()),table_Courses.getValueAt(table_Courses.getSelectedRow(), 5).toString()));
               combo_Teacher.getModel().setSelectedItem(new ComboboxItem(Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 6).toString()),(table_Courses.getValueAt(table_Courses.getSelectedRow(), 7).toString() + " " + table_Courses.getValueAt(table_Courses.getSelectedRow(), 8).toString())));
//                System.out.println(new ComboboxItem(Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 6).toString()),(table_Courses.getValueAt(table_Courses.getSelectedRow(), 7).toString() + " " + table_Courses.getValueAt(table_Courses.getSelectedRow(), 8).toString())));

            }
        });
        deleteButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int ID = Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString());
                try {
                    String query = "Delete From Grades where CourseID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, ID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }try {
                    String query = "Delete From Open_Courses where CourseID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, ID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }try {
                    String query = "Delete From Open_Courses where CourseID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, ID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }try {
                    String query = "Delete From Courses where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setInt(1, ID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }
                TeacherCourseManagement win = new TeacherCourseManagement();
                frame.dispose();
            }
        });
        updateButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                int ID = Integer.parseInt(table_Courses.getValueAt(table_Courses.getSelectedRow(), 0).toString());
                String name = text_Name.getText();
                String semester = text_Semester.getText();
                String credit = text_Credit.getText();
                int departmentID = ((ComboboxItem)combo_Department.getSelectedItem()).getId();
                int teacherID = ((ComboboxItem)combo_Teacher.getSelectedItem()).getId();


                try {
                    String query = "update Courses set name = ? , Semester = ? , Credit = ? , DepartmentID = ? , TeacherID = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, name);
                    pst.setString(2, semester);
                    pst.setString(3, credit);
                    pst.setInt(4, departmentID);
                    pst.setInt(5, teacherID);
                    pst.setInt(6, ID);
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "error");
                }
                TeacherCourseManagement win = new TeacherCourseManagement();
                frame.dispose();
            }
        });
    }

    void fillData() {
        try {
            DefaultTableModel model2 = (DefaultTableModel) table_Courses.getModel();
            model2.setRowCount(0);

            DefaultComboBoxModel model = new DefaultComboBoxModel();
            String query = "select ID, Name from Departments";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            while (rs.next()) {
                int ID = rs.getInt(1);
                String name = rs.getString(2);
                ComboboxItem item = new ComboboxItem(ID, name);
                model.addElement(item);
            }
            combo_Department.setModel(model);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
        try {
            DefaultComboBoxModel model = new DefaultComboBoxModel();
            String query = "select ID, Name , Surname from Teachers";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            while (rs.next()) {
                int ID = rs.getInt(1);
                String name = rs.getString(2);
                String surname = rs.getString(3);
                ComboboxItem item = new ComboboxItem(ID, name + " " + surname);
                model.addElement(item);
            }
            combo_Teacher.setModel(model);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
        try {
            String query = "select Courses.ID, Courses.Name,Courses.Semester,Courses.Credit,Courses.DepartmentID,Departments.name,Courses.TeacherID,Teachers.name as 'Teacher Name', Teachers.surname as 'Teacher Surname' from courses left join Departments on Courses.DepartmentID = Departments.ID Left join Teachers on Courses.TeacherID = Teachers.ID";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
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
