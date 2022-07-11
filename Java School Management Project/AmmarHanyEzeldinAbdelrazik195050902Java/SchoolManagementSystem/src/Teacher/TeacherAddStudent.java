package Teacher;

import Database.DbConnection;
import Publics.PublicFunctions;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherAddStudent {
    private JButton button_Back;
    private JPanel mainPanel;
    private JLabel label_Name;
    private JTextField text_Name;
    private JButton button_Add;
    private JTextField text_Surname;
    private JTextField text_Username;
    private JTextField text_Password;
    private JComboBox Combobox_Department;
    private JTextField text_Email;
    private JTextField text_Mobile;
    private JTextField text_Gender;
    private JTextField text_Birthday;
    private JComboBox combo_Gender;

    public TeacherAddStudent() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);
        Populate();


        combo_Gender.addItem("Male");
        combo_Gender.addItem("Female");


        frame.setVisible(true);


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                TeacherStudents win = new TeacherStudents();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
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
                String department = Combobox_Department.getSelectedItem().toString();
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
                    String query2 = "Insert Into Students (name,surname,username,password,email,mobile,gender,birthday,DepartmentID) values(?,?,?,?,?,?,?,?,?)";
                    PreparedStatement pst2 = con.prepareStatement(query2);
                    pst2.setString(1,name);
                    pst2.setString(2,surname);
                    pst2.setString(3,username);
                    pst2.setString(4,password);
                    pst2.setString(5,email);
                    pst2.setString(6,mobile);
                    pst2.setString(7,gender);
                    pst2.setString(8,birthday);
                    pst2.setInt(9,departmentID);
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
                Combobox_Department.addItem(name);

            }
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox("Error " + x.toString(), "Error");
        }
    }
}
