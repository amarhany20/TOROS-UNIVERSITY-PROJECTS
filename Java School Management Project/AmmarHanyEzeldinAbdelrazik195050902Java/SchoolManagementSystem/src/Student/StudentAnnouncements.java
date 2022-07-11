package Student;

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

public class StudentAnnouncements {
    private JTable table_Announcements;

    private JPanel mainPanel;
    private JButton button_Back;
    private JTextArea textarea_Announcement;

    public boolean isCellEditable(int row, int column) {
        return false;
    }

    public StudentAnnouncements() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Announcements.setDefaultEditor(Object.class, null);
        frame.setVisible(true);


        try {
            String query = "Select Announcements.ID, Announcements.title,Announcements.Date,Teachers.name as 'Teacher',Departments.name as 'Department',Faculties.name as 'Faculty',Announcements.Announcement from announcements " +
                    "INNER JOIN TEACHERS ON Announcements.TeacherID = Teachers.ID " +
                    "INNER JOIN Departments ON Announcements.DepartmentID = Departments.ID " +
                    "INNER JOIN Faculties ON Departments.FacultyID = Faculties.ID " +
                    "where Faculty like ?";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement st = con.prepareStatement(query);
            st.setString(1, PublicVariables.student.getFaculty());
            ResultSet rs = st.executeQuery();
            DbConnection.resultSetToTableModel(rs, table_Announcements);
            table_Announcements.getColumnModel().getColumn(6).setMinWidth(1);
            table_Announcements.getColumnModel().getColumn(6).setMaxWidth(1);
//                table_Announcements.setAutoResizeMode(1);

        } catch (
                Exception x) {
            PublicFunctions.infoBox("Error at Sudents Announcements\n" + x, "Error at Sudents Announcements");
        }


        table_Announcements.getSelectionModel().addListSelectionListener(new ListSelectionListener() {

                    public void valueChanged(ListSelectionEvent lse) {
                        if (!lse.getValueIsAdjusting()) {
                            int row = table_Announcements.getSelectedRow();
                            int column = table_Announcements.getSelectedColumn();
                            String anoucement = (String) table_Announcements.getValueAt(row, 6);
                            textarea_Announcement.setText(anoucement);
                        }
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

    // Functions


}
