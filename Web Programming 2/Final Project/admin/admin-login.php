<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="icon" href="/favicon/favicon.ico" />
  <link rel="stylesheet" href="/stylesheets/styles.css" type="text/css">
  <link rel="preconnect" href="https://fonts.gstatic.com">
  <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
  <title>Admin Login 🍕</title>
</head>

<body class="home-body">
  <div class="home-top-container">
    <h1>Welcome To Pizza Restaurant</h1>
    <h2>The Best Pizza is on the way.</h2>
    <h3>Now available in Mersin</h3>
    <div class="home-navigation-bar">
      <a class="home-links" href="">Home</a>
      <a class="home-links" href="">About Us</a>
      <a class="home-links" href="">Contact Us</a>
    </div>
  </div>
  <div class="overlay-color"></div>
    <div style="background-color: black;width: 30%;height: 40px;margin: auto;padding-top: 20px;filter:drop-shadow(0px 0px 30px black)">
    <form method="POST" name="admin-login" action="">
    Administrator: 
    <input type="text" name="administrator" id="">
    <input type="submit" name="submit">
    </form>
    </div>
    <?php
    if(isset($_POST['submit'])){
        $admin = $_POST['administrator'];
        if ($admin == null) {
            $message = "admin is empty. ";
            echo "<script type='text/javascript'>alert('$message');</script>";
            header("refresh:0.1;url=/admin/admin-login.php");
            return;
        }
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = mysqli_connect($servername, $username, $password, $dbname);
        // Check connection
        if (!$conn) {
            die("Connection failed: " . mysqli_connect_error());
        }
        $sql = "SELECT * FROM admin WHERE admin='$admin'";
        $result = $conn->query($sql);
        if ($result->num_rows > 0) {
            // output data of each row
            while ($row = $result->fetch_assoc()) {
                $message = "Welcome " . $row["admin"];
                echo "<script type='text/javascript'>alert('$message');</script>";
                session_start();
                $_SESSION['admin'] = $row["admin"];
                $_SESSION['admin-id'] = $row["id"];
                header("refresh:0.1;url=/admin/admin-menu.php");
            }
        } else {
            $message = "administrator not found. If you forgot your admin you should contact this software support. ";
            echo "<script type='text/javascript'>alert('$message');</script>";
            header("refresh:0.1;url=/admin/admin-login.php");
        }

        mysqli_close($conn);
    }
    ?>

</body>
</html>