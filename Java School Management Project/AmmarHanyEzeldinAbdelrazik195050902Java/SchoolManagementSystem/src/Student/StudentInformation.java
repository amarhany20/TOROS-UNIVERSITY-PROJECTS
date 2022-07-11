package Student;

import Publics.PublicVariables;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class StudentInformation {
    private JPanel mainPanel;
    private JButton backButton;
    private JLabel label_Name;
    private JLabel label_Surname;
    private JLabel label_Username;
    private JLabel label_DateOfBirth;
    private JLabel label_Gender;
    private JLabel label_Mobile;
    private JLabel label_Department;
    private JLabel label_Faculty;
    private JLabel datalabel_Name;
    private JLabel datalabel_Surname;
    private JLabel datalabel_Username;
    private JLabel datalabel_Gender;
    private JLabel datalabel_Mobile;
    private JLabel datalabel_Department;
    private JLabel datalabel_Faculty;
    private JLabel datalabel_DateOfBirth;
    private JLabel label_Supervisor;
    private JLabel datalabel_Supervisor;
    private JLabel label_GPA;
    private JLabel datalabel_GPA;
    private JLabel label_Email;
    private JLabel datalabel_Email;

    public StudentInformation() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        datalabel_Name.setText(PublicVariables.student.getName());
        datalabel_Surname.setText(PublicVariables.student.getSurname());
        datalabel_Username.setText(PublicVariables.student.getUsername());
        datalabel_Email.setText(PublicVariables.student.getEmail());
        datalabel_Mobile.setText(PublicVariables.student.getMobile());
        datalabel_Gender.setText(PublicVariables.student.getGender());
        datalabel_DateOfBirth.setText(PublicVariables.student.getBirthday());
        datalabel_Department.setText(PublicVariables.student.getDepartment());
        datalabel_Faculty.setText(PublicVariables.student.getFaculty());
        datalabel_Supervisor.setText(PublicVariables.student.getSupervisor());
        datalabel_GPA.setText(PublicVariables.student.getGPA());

//        try {
//            Connection con = DbConnection.ConnectionDB();
//            String query = "Select * from users where ID like ?";
//            PreparedStatement st = con.prepareStatement(query);
//            st.setInt(1, PublicVariables.LoggedInID);
//            ResultSet rs = st.executeQuery();
////            while (rs.next()){
////
////            }
//            datalabel_Name.setText(rs.getString(2));
//            datalabel_Surname.setText(rs.getString(3));
//            datalabel_Username.setText(rs.getString(4));
//            datalabel_DateOfBirth.setText(rs.getString(6));
//            datalabel_Gender.setText(rs.getString(7));
//            datalabel_Mobile.setText(rs.getString(8));
//            datalabel_Department.setText(rs.getString(9));
//            datalabel_Faculty.setText(rs.getString(10));
//
//        } catch (Exception x) {
//            PublicFunctions.infoBox("Error in Database\n" + x, "Error In Student Information");
//            StudentMainMenu win = new StudentMainMenu();
//            frame.dispose();
//        }
        frame.setVisible(true);


        backButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                StudentMainMenu win = new StudentMainMenu();
                frame.dispose();

            }
        });
    }
}
