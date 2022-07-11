package Teacher;

import Database.DbConnection;
import Publics.ComboboxItem;
import Publics.PublicFunctions;

import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class TeacherFacultyManagement {
    private JButton button_Back;
    private JTable table_Faculties;
    private JTextField text_Name;
    private JComboBox combo_Teacher;
    private JButton button_Update;
    private JButton button_Delete;
    private JButton button_Add;
    private JPanel mainPanel;

    public TeacherFacultyManagement() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Faculties.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Faculties.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 0).toString();
                text_Name.setText(table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 1).toString());

                combo_Teacher.getModel().setSelectedItem(new ComboboxItem(Integer.parseInt(table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 2).toString()), ( table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 3).toString() + " " + table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 4).toString() ) ));
            }
        });


        button_Back.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {

                TeacherMainMenu win = new TeacherMainMenu();
                frame.dispose();
            }
        });
        button_Update.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Update Faculties set name = ? , TeacherID = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Name.getText());
                    pst.setInt(2, ((ComboboxItem) combo_Teacher.getSelectedItem()).getId());
                    pst.setInt(3, Integer.parseInt(table_Faculties.getValueAt(table_Faculties.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherFacultyManagement win = new TeacherFacultyManagement();
                frame.dispose();
            }
        });
        button_Delete.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
//                try{
//                    String query = "Insert into Departments (name,facultyID) values (?,?)";
//                    Connection con = DbConnection.ConnectionDB();
//                    PreparedStatement pst = con.prepareStatement(query);
//                    pst.setString(1,text_Name.getText());
//                    pst.setInt(2,((ComboboxItem)combo_Faculty.getSelectedItem()).getId());
//                    pst.executeUpdate();
//                    pst.close();
//                    con.close();
//                }catch (Exception x){
//                    PublicFunctions.infoBox(x.toString(),"Error");
//                }
                PublicFunctions.infoBox("I have disabled Delete Faculties because it deleted every foriegn key linked to it", "STOP!!!!!!!!!!!!");
                TeacherFacultyManagement win = new TeacherFacultyManagement();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Insert into Faculties (name,TeacherID) values (?,?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Name.getText());
                    pst.setInt(2, ((ComboboxItem) combo_Teacher.getSelectedItem()).getId());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }
                TeacherFacultyManagement win = new TeacherFacultyManagement();
                frame.dispose();
            }
        });
    }

    void fillData() {
        try {
            DefaultComboBoxModel model = new DefaultComboBoxModel();
            String query = "select ID, Name, Surname from Teachers ";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            while (rs.next()) {
                int ID = rs.getInt(1);
                String name = rs.getString(2) + " " + rs.getString(3);
                ComboboxItem item = new ComboboxItem(ID, name);
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
            String query = "select Faculties.ID, Faculties.Name,Faculties.TeacherID,Teachers.Name, Teachers.Surname from Faculties Left join Teachers on Faculties.TeacherID = Teachers.ID";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Faculties);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}


