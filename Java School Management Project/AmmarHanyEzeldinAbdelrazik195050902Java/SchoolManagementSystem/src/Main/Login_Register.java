package Main;

import Database.DbConnection;
import Models.Student;
import Models.Teacher;
import Publics.PublicFunctions;
import Publics.PublicVariables;
import Student.StudentMainMenu;
import Teacher.TeacherMainMenu;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class Login_Register {

    private JTextField text_LoginUsername;
    private JPasswordField password_LoginPassword;
    private JButton button_Login;
    private JLabel label_LoginUsername;
    private JLabel label_LoginPassword;
    private JPanel panel_Login;
    private JButton button_Register;
    private JPanel mainPanel;
    private JComboBox combo_Type;

    public Login_Register() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        JRootPane buttonRootPane = SwingUtilities.getRootPane(button_Login);
        buttonRootPane.setDefaultButton(button_Login);

        combo_Type.addItem("Student");
        combo_Type.addItem("Teacher");

        frame.setVisible(true);


        button_Login.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                Connection con = null;
                PreparedStatement pst = null;
                ResultSet rs = null;
                String query = "";
                if (combo_Type.getSelectedIndex() == 0) {
                    query = "Select Students.ID,Students.Name,Students.Surname,Students.Username,Students.Password,Students.Email,Students.Mobile,Students.Gender,Students.Birthday,Students.DepartmentID, Departments.name as 'Department',Departments.FacultyID,Faculties.name as 'Faculty', Teachers.name as 'Supervisor',Students.GPA as 'GPA'\n" +
                            "from Students \n" +
                            "LEFT JOIN Departments\n" +
                            "ON Students.DepartmentID = Departments.ID\n" +
                            "LEFT JOIN Teachers \n" +
                            "On Students.SupervisorID = Teachers.ID\n" +
                            "LEFT JOIN Faculties\n" +
                            "ON Departments.FacultyID = Faculties.ID where Students.username like ? AND Students.password like ?;";
                } else {
                    query = "Select Teachers.ID,Teachers.name,Teachers.Surname,Teachers.Username,Teachers.Password,Teachers.DepartmentID,Departments.name as 'Department',Faculties.ID,Faculties.name as 'Faculty' from Teachers\n" +
                            "LEFT JOIN Departments\n" +
                            "ON Teachers.DepartmentID = Departments.ID\n" +
                            "LEFT JOIN Faculties\n" +
                            "ON Departments.FacultyID = Faculties.ID Where Teachers.Username like ? AND Teachers.password like ?;";
                }
                String username = text_LoginUsername.getText().trim();
                String password = new String(password_LoginPassword.getPassword());

                if (username.isEmpty() || password.isEmpty()) {
                    PublicFunctions.infoBox("Username or Password shouldn't be empty", "Failed");
                    return;
                }

                try {
                    con = DbConnection.ConnectionDB();
                    pst = con.prepareStatement(query);
                    pst.setString(1, username);
                    pst.setString(2, password);
                    rs = pst.executeQuery();


//                    ResultSetMetaData rsmd = rs.getMetaData();
//                    int columnCount = rsmd.getColumnCount();
                    //get columns
//                    ArrayList<String> hotelResultList = new ArrayList<>(columnCount);
//                    while (rs.next()) {
//                        int i = 1;
//                        while (i <= columnCount) {
//                            hotelResultList.add(rs.getString(i++));
//                        }
//                    }
//                    System.out.println(hotelResultList);

                    if (rs.next()) {
                        PublicFunctions.infoBox("Login Successful!, Welcome " + rs.getString(2) + " " + rs.getString(3), "Success");
//                        rs.getInt(1);
//                        rs.getInt("userid"); by name of column
                        PublicVariables.LoginSuccessful = true;
                        PublicVariables.LoggedInID = rs.getInt(1);
                        PublicVariables.LoggedInUsername = rs.getString(4);

                        if (PublicVariables.LoginSuccessful) {// Sign in successful
                            if (combo_Type.getSelectedIndex() == 0) {
                                PublicVariables.student = new Student(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getString(4), rs.getString(6), rs.getString(7), rs.getString(8), rs.getString(9), rs.getInt(10), rs.getString(11), rs.getInt(12), rs.getString(13), rs.getString(14), rs.getString(15));
                                con.close();
                                frame.dispose();
                                StudentMainMenu win = new StudentMainMenu();
                            } else {

                                PublicVariables.teacher = new Teacher(rs.getInt(1), rs.getString(2), rs.getString(3), rs.getString(4), rs.getInt(6), rs.getString(7), rs.getInt(8), rs.getString(9));
                                con.close();
                                frame.dispose();

                                TeacherMainMenu win = new TeacherMainMenu();
                            }
                        }

                    } else {
                        PublicFunctions.infoBox("Wrong Username Or Password", "Failed");
                        return;
                    }

                } catch (Exception x) {

                    PublicFunctions.infoBox("Failed \n" + x, "Failed At Login");
                }
            }
        });

    }


    public static void main(String[] args) {
        Login_Register win = new Login_Register();

    }
}
