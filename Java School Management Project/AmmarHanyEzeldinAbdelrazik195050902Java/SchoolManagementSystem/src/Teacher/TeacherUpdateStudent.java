package Teacher;

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

public class TeacherUpdateStudent {
    private JTextField text_Name;
    private JButton button_Back;
    private JComboBox combo_Gender;
    private JTextField text_Surname;
    private JTextField text_Username;
    private JTextField text_Email;
    private JTextField text_Mobile;
    private JTextField text_Birthday;
    private JComboBox combo_Department;
    private JTextField text_Password;
    private JButton button_Update;
    private JPanel mainPanel;


    public TeacherUpdateStudent() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280,720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width/2-frame.getSize().width/2, dim.height/2-frame.getSize().height/2);

        frame.setVisible(true);

        combo_Gender.addItem("Male");
        combo_Gender.addItem("Female");
        Populate();
        fillData();

        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherStudents win = new TeacherStudents();
                 frame.dispose();
            }
        });


        button_Update.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                String name = text_Name.getText().trim();
                String surname = text_Surname.getText().trim();
                String username = text_Username.getText().trim();
                String password = text_Password.getText().trim();
                String email = text_Email.getText().trim();
                String mobile = text_Mobile.getText().trim();
                String gender = combo_Gender.getSelectedItem().toString();
                String birthday = text_Birthday.getText().trim();
                String department = combo_Department.getSelectedItem().toString();
                int departmentID = -1;


                try {
                    Connection con = DbConnection.ConnectionDB();
                    String query1  = "Select ID from Departments where name like ?";
                    PreparedStatement pst = con.prepareStatement(query1);
                    pst.setString(1,department);
                    ResultSet rs = pst.executeQuery();
                    while (rs.next()){
                        departmentID =  rs.getInt(1);
                    }
                    rs.close();
                    con.close();
                    con = DbConnection.ConnectionDB();
                    String query2 = "Update Students set name = ? , surname = ? , username = ? , password = ? , email = ? , mobile = ? , gender = ? , birthday = ? where ID like ?";
                    PreparedStatement pst2 = con.prepareStatement(query2);
                    pst2.setString(1,name);
                    pst2.setString(2,surname);
                    pst2.setString(3,username);
                    pst2.setString(4,password);
                    pst2.setString(5,email);
                    pst2.setString(6,mobile);
                    pst2.setString(7,gender);
                    pst2.setString(8,birthday);
                    pst2.setInt(9,PublicVariables.UpdateStudentID);
                    pst2.executeUpdate();
                    PublicFunctions.infoBox("User has been added successfully","");

                    TeacherStudents win = new TeacherStudents();
                    frame.dispose();

                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(),"Error");
                }


            }
        });
    }
    void Populate() {
        try {
            Connection con = DbConnection.ConnectionDB();
            String query = "Select ID,Name from Departments";
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            while (rs.next()) {
                int id = rs.getInt(1);
                String name = rs.getString(2);
                combo_Department.addItem(name);

            }
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox("Error " + x.toString(), "Error");
        }
    }
    public void fillData(){
        try{

            Connection con = DbConnection.ConnectionDB();
            String query = "Select students.name,students.surname,students.username,students.password,students.email,students.mobile,students.gender,students.birthday,Departments.name from Students left join Departments on Students.DepartmentID = Departments.ID where Students.ID like ?";
            PreparedStatement pst = con.prepareStatement(query);
            pst.setInt(1, PublicVariables.UpdateStudentID);
            ResultSet rs = pst.executeQuery();
            while (rs.next()){
                text_Name.setText(rs.getString(1));
                text_Surname.setText(rs.getString(2));
                text_Username.setText(rs.getString(3));
                text_Password.setText(rs.getString(4));
                text_Email.setText(rs.getString(5));
                text_Mobile.setText(rs.getString(6));
                combo_Gender.setSelectedItem(rs.getString(7));
                text_Birthday.setText(rs.getString(8));
                combo_Department.setSelectedItem(rs.getString(9));
            }
            con.close();

        }catch (Exception x){
            PublicFunctions.infoBox(x.toString(),"Error");
        }
    }
}
