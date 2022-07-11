package Models;

public class Teacher {
    private final int ID;
    private final String Name;
    private final String Surname;
    private final String Username;
    private final int DepartmentID;
    private final String Department;
    private final int FacultyID;
    private final String Faculty;

    public int getID() {
        return ID;
    }

    public String getName() {
        return Name;
    }

    public String getSurname() {
        return Surname;
    }

    public String getUsername() {
        return Username;
    }

    public String getDepartment() {
        return Department;
    }

    public String getFaculty() {
        return Faculty;
    }

    public int getDepartmentID() {
        return DepartmentID;
    }

    public int getFacultyID() {
        return FacultyID;
    }

    public Teacher(int ID, String name, String surname, String username, int departmentID, String department, int facultyID, String faculty) {
        this.ID = ID;
        Name = name;
        Surname = surname;
        Username = username;
        DepartmentID = departmentID;
        Department = department;
        FacultyID = facultyID;
        Faculty = faculty;
    }
}
