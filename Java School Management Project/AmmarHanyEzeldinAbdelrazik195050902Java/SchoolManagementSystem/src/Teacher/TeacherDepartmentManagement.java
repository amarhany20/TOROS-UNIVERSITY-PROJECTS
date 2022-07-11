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

public class TeacherDepartmentManagement {
    private JPanel mainPanel;
    private JButton button_Back;
    private JTable table_Departments;
    private JTextField text_Name;
    private JComboBox combo_Faculty;
    private JButton button_Update;
    private JButton button_Delete;
    private JButton button_Add;

    public TeacherDepartmentManagement() {
        JFrame frame = new JFrame();
        frame.setContentPane(mainPanel);
        frame.setTitle("Student Management System");
        frame.setSize(1280, 720);
        frame.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);

        Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
        frame.setLocation(dim.width / 2 - frame.getSize().width / 2, dim.height / 2 - frame.getSize().height / 2);

        table_Departments.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);

        frame.setVisible(true);

        fillData();


        table_Departments.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
            public void valueChanged(ListSelectionEvent event) {
                String ID = table_Departments.getValueAt(table_Departments.getSelectedRow(), 0).toString();
                text_Name.setText(table_Departments.getValueAt(table_Departments.getSelectedRow(), 1).toString());

                combo_Faculty.getModel().setSelectedItem(new ComboboxItem(Integer.parseInt(table_Departments.getValueAt(table_Departments.getSelectedRow(), 2).toString()), table_Departments.getValueAt(table_Departments.getSelectedRow(), 3).toString()));
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
                    String query = "Update Departments set name = ? , FacultyID = ? where ID like ?";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Name.getText());
                    pst.setInt(2, ((ComboboxItem) combo_Faculty.getSelectedItem()).getId());
                    pst.setInt(3, Integer.parseInt(table_Departments.getValueAt(table_Departments.getSelectedRow(), 0).toString()));
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }

                TeacherDepartmentManagement win = new TeacherDepartmentManagement();
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
                PublicFunctions.infoBox("I have disabled Delete Department because it deleted every foriegn key linked to it", "STOP!!!!!!!!!!!!");
                TeacherDepartmentManagement win = new TeacherDepartmentManagement();
                frame.dispose();
            }
        });
        button_Add.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                try {
                    String query = "Insert into Departments (name,facultyID) values (?,?)";
                    Connection con = DbConnection.ConnectionDB();
                    PreparedStatement pst = con.prepareStatement(query);
                    pst.setString(1, text_Name.getText());
                    pst.setInt(2, ((ComboboxItem) combo_Faculty.getSelectedItem()).getId());
                    pst.executeUpdate();
                    pst.close();
                    con.close();
                } catch (Exception x) {
                    PublicFunctions.infoBox(x.toString(), "Error");
                }
                TeacherDepartmentManagement win = new TeacherDepartmentManagement();
                frame.dispose();
            }
        });
    }

    void fillData() {
        try {
            DefaultComboBoxModel model = new DefaultComboBoxModel();
            String query = "select ID, Name from Faculties";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            while (rs.next()) {
                int ID = rs.getInt(1);
                String name = rs.getString(2);
                ComboboxItem item = new ComboboxItem(ID, name);
                model.addElement(item);
            }
            combo_Faculty.setModel(model);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
        try {
            String query = "select Departments.ID, Departments.Name,Departments.FacultyID,Faculties.Name from Departments Left join Faculties on Departments.FacultyID = Faculties.ID";
            Connection con = DbConnection.ConnectionDB();
            PreparedStatement pst = con.prepareStatement(query);
            ResultSet rs = pst.executeQuery();
            DbConnection.resultSetToTableModel2(rs, table_Departments);
            pst.close();
            rs.close();
            con.close();
        } catch (Exception x) {
            PublicFunctions.infoBox(x.toString(), "error");
        }
    }
}


