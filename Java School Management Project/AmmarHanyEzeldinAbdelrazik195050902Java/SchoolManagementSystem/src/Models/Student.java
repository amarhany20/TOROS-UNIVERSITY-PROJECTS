package Models;

public class Student {
    private final int ID;
    private final String Name;
    private final String Surname;
    private final String Username;
    private final String Email;

    private final String Mobile;


    private final String Gender;
    private final String Birthday;
    private final int DepartmentID;
    private final String Department;


    private final int FacultyID;
    private final String Faculty;
    private final String Supervisor;
    private final String GPA;

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

    public String getEmail() {
        return Email;
    }

    public String getMobile() {
        return Mobile;
    }

    public String getGender() {
        return Gender;
    }

    public String getBirthday() {
        return Birthday;
    }

    public String getDepartment() {
        return Department;
    }

    public String getFaculty() {
        return Faculty;
    }

    public String getSupervisor() {
        return Supervisor;
    }

    public String getGPA() {
        return GPA;
    }

    public int getDepartmentID() {
        return DepartmentID;
    }

    public int getFacultyID() {
        return FacultyID;
    }

    public Student(int ID, String name, String surname, String username, String email, String mobile, String gender, String birthday, int departmentID, String department, int facultyID, String faculty, String supervisor, String GPA) {
        this.ID = ID;
        Name = name;
        Surname = surname;
        Username = username;
        Email = email;
        Mobile = mobile;
        Gender = gender;
        Birthday = birthday;
        DepartmentID = departmentID;
        Department = department;
        FacultyID = facultyID;
        Faculty = faculty;
        Supervisor = supervisor;
        this.GPA = GPA;
    }
}
