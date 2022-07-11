<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="/stylesheets/styles.css" type="text/css">
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
    <title>Edit Stocks</title>
</head>

<body class="home-body">
    <div class="home-top-container">
        <h1>Welcome To Pizza Restaurant</h1>
        <h2>The Best Pizza is on the way.</h2>
        <h3>Now available in Mersin</h3>
        <div class="home-navigation-bar">
            <form name="signout" method="GET" action=""><button class="home-links" style="background-color: gray;" name="signout" type="submit" class=""> Sign out</button></form>

            <?php if (isset($_GET['signout'])) {
                session_destroy();
                header("Location: /index.php");
            } ?>

            <?php

            session_start();
            $username = $_SESSION['admin'];
            $customerNumber = $_SESSION['admin-id'];
            echo "<h3 class=\"home-links\" style=\"display: inline; text-align: right; position: absolute; top: 145px; right: 10px;\" >$username - $customerNumber</h3>";
            ?>
        </div>
        <form action="" method="POST"><input style="margin:auto;" type="submit" name="create" value="Add a new item."></form>
        <?php
        if (isset($_POST['create'])) {
            echo "<label for=\"type\">Choose your new type </label><br>
            <form action=\"\" method=\"POST\">
                <select name=\"type\" id=\"\">
                    <option value=\"pizza\">pizza</option>
                    <option value=\"beverage\">beverage</option>
                    <option value=\"dessert\">dessert</option>
                </select>
                <input type=\"submit\" name=\"create_type\" value=\"select\">
            </form>";
        }
        if (isset($_POST['create_type'])) {
            $newtype = $_POST['type'];
            $_SESSION['newtype'] = $newtype;

            if ($newtype == "pizza") {
                echo "
                <form action=\"\" method=\"POST\">
                <p> Add Pizza</p>
                
       <table style=\"margin:auto\">
       <thead>
       <th>type</th>
       <th>Small Price</th>
       <th>Small Availability</th>
       <th>Medium Price</th>
       <th>Medium Availibility</th>
       <th>Large Price</th>
       <th>Large Availability</th>
       </thead>
       <tr>
       <td><input value=\"\" type=\"text\" name=\"type\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"small_Price\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"smallAvailability\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"medium_Price\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"mediumAvailability\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"large_Price\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"largeAvailablitiy\" id=\"\"></td>
       </tr>
       </table>
       <input type=\"submit\" name=\"create_pizza\" value=\"Create Pizza\">
       </form>
                ";
            }
            if ($newtype == "beverage") {
                echo "
                <form action=\"\" method=\"POST\">
                <p> Add beverage</p>
                
       <table style=\"margin:auto\">
       <thead>
       <th>type</th>
       <th>Price</th>
       <th>Availability</th>
       </thead>
       <tr>
       <td><input value=\"\" type=\"text\" name=\"type\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"price\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"availability\" id=\"\"></td>
       </tr>
       </table>
       <input type=\"submit\" name=\"create_beverage\" value=\"Create Beverage\">
       </form>
                ";
            }
            if ($newtype == "dessert") {
                echo "
                <form action=\"\" method=\"POST\">
                <p> Add dessert</p>
                
       <table style=\"margin:auto\">
       <thead>
       <th>type</th>
       <th>Price</th>
       <th>Availability</th>
       </thead>
       <tr>
       <td><input value=\"\" type=\"text\" name=\"type\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"price\" id=\"\"></td>
       <td><input value=\"\" type=\"number\" name=\"availability\" id=\"\"></td>
       </tr>
       </table>
       <input type=\"submit\" name=\"create_dessert\" value=\"Create Dessert\">
       </form>
                ";
            }
        }
        if (isset($_POST['create_pizza'])) {
            $newtype = $_SESSION['newtype'];

            $pizza_type = $_POST["type"];
            $small_Price = $_POST["small_Price"];
            $smallAvailability = $_POST["smallAvailability"];
            $medium_Price = $_POST["medium_Price"];
            $mediumAvailability = $_POST["mediumAvailability"];
            $large_Price = $_POST["large_Price"];
            $largeAvailablitiy = $_POST["largeAvailablitiy"];

            $servername = "localhost";
            $username = "root";
            $password = "";
            $dbname = "pizza";

            // Create connection
            $conn = new mysqli($servername, $username, $password, $dbname);
            // Check connection
            if ($conn->connect_error) {
                die("Connection failed: " . $conn->connect_error);
            }

            $sql = "INSERT INTO pizza (type, small_Price, smallAvailability,medium_Price,mediumAvailability,large_Price,largeAvailablitiy)
VALUES ('$pizza_type','$small_Price','$smallAvailability','$medium_Price','$mediumAvailability','$large_Price','$largeAvailablitiy')";

            if ($conn->query($sql) === TRUE) {
                echo "New pizza has been added successfully";
            } else {
                echo "Error: " . $sql . "<br>" . $conn->error;
            }

            $conn->close();
        }
        if (isset($_POST['create_beverage'])) {

            $newtype = $_SESSION['newtype'];

            $beverage_type = $_POST["type"];
            $Price = $_POST["price"];
            $Availability = $_POST["availability"];

            $servername = "localhost";
            $username = "root";
            $password = "";
            $dbname = "pizza";

            // Create connection
            $conn = new mysqli($servername, $username, $password, $dbname);
            // Check connection
            if ($conn->connect_error) {
                die("Connection failed: " . $conn->connect_error);
            }

            $sql = "INSERT INTO beverage (type, price, availability)
VALUES ('$beverage_type','$Price','$Availability')";

            if ($conn->query($sql) === TRUE) {
                echo "New beverage has been added successfully";
            } else {
                echo "Error: " . $sql . "<br>" . $conn->error;
            }
        }
        if (isset($_POST['create_dessert'])) {

            $newtype = $_SESSION['newtype'];

            $dessert_type = $_POST["type"];
            $Price = $_POST["price"];
            $Availability = $_POST["availability"];

            $servername = "localhost";
            $username = "root";
            $password = "";
            $dbname = "pizza";

            // Create connection
            $conn = new mysqli($servername, $username, $password, $dbname);
            // Check connection
            if ($conn->connect_error) {
                die("Connection failed: " . $conn->connect_error);
            }

            $sql = "INSERT INTO dessert (type, price, availability)
VALUES ('$dessert_type','$Price','$Availability')";

            if ($conn->query($sql) === TRUE) {
                echo "New dessert has been added successfully";
            } else {
                echo "Error: " . $sql . "<br>" . $conn->error;
            }
        }
        ?>
    </div>

    <div class="overlay-color"></div>
    <div class="ordering2-overlay-color"></div>


    <label for="type">Choose type to edit </label><br>
    <form action="" method="POST">
        <select name="type" id="">
            <option value="pizza">pizza</option>
            <option value="beverage">beverage</option>
            <option value="dessert">dessert</option>
        </select>
        <input type="submit" name="select_type" value="select">
    </form>

    <?php
    if (isset($_POST['select_type'])) {

        $type = $_POST['type'];
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "SELECT * FROM $type";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row 
            echo "<form action=\"\" method=\"POST\">
             <p> Select $type</p>
            <select name=\"type2\" id=\"\">";
            while ($row = $result->fetch_assoc()) {
                echo "
        <option name=\"" . $row["id"] . "\" value=\"" . $row["id"] . "\">" . $row["type"] . "</option>
          ";
            }
            echo "
            
      </select><input type=\"submit\" name=\"select_$type\" value=\"select\"></form>
      ";
            $_SESSION["stock-type"] = $type;
        } else {
            echo "0 results";
        }
        $conn->close();
    }

    if (isset($_POST['select_pizza'])) {
        $type = $_SESSION["stock-type"];
        $id =  $_POST['type2'];
        $_SESSION['item_id'] = $id;
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "SELECT * FROM $type where id = $id";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row 
            echo "<form action=\"\" method=\"POST\">
             <p> Edit or Delete</p>
             
    <table style=\"margin:auto\">
    <thead>
    <th>type</th>
    <th>Small Price</th>
    <th>Small Availability</th>
    <th>Medium Price</th>
    <th>Medium Availibility</th>
    <th>Large Price</th>
    <th>Large Availability</th>
    </thead>

            ";
            while ($row = $result->fetch_assoc()) {
                echo "    <td> <input value=\"" . $row["type"] . "\" type=\"text\" name=\"type\" id=\"\"></td>
                <td> 
                <input value=\"" . $row["small_Price"] . "\" type=\"number\" name=\"small_Price\" id=\"\"></td>
                <td><input value=\"" . $row["smallAvailability"] . "\" type=\"number\" name=\"smallAvailability\" id=\"\"></td>
                <td><input value=\"" . $row["medium_Price"] . "\" type=\"number\" name=\"medium_Price\" id=\"\"></td>
                <td><input value=\"" . $row["mediumAvailability"] . "\" type=\"number\" name=\"mediumAvailability\" id=\"\"></td>
                <td><input value=\"" . $row["large_Price"] . "\" type=\"number\" name=\"large_Price\" id=\"\"></td>
                <td><input value=\"" . $row["largeAvailablitiy"] . "\" type=\"number\" name=\"largeAvailablitiy\" id=\"\"></td>
                </table>";
            }
            echo "
            
    <input type=\"submit\" name=\"edit_$type\" value=\"Apply Changes\">
    <input type=\"submit\" name=\"delete_$type\" value=\"Delete this type\">
            </form>
      ";
        } else {
            echo "0 results";
        }


        $conn->close();
    }

    if (isset($_POST["select_beverage"])) {
        $type = $_SESSION["stock-type"];
        $id =  $_POST['type2'];
        $_SESSION['item_id'] = $id;
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "SELECT * FROM $type where id = $id";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row 
            echo "<form action=\"\" method=\"POST\">
             <p> Edit or Delete</p>
             
    <table style=\"margin:auto\">
    <thead>
    <th>type</th>
    <th>Price</th>
    <th>Availability</th>
    </thead>

            ";
            while ($row = $result->fetch_assoc()) {
                echo "    <td> <input value=\"" . $row["type"] . "\" type=\"text\" name=\"type\" id=\"\"></td>
                <td> 
                <input value=\"" . $row["price"] . "\" type=\"number\" name=\"price\" id=\"\"></td>
                <td><input value=\"" . $row["availability"] . "\" type=\"number\" name=\"availability\" id=\"\"></td>
                </table>";
            }
            echo "
            
    <input type=\"submit\" name=\"edit_$type\" value=\"Apply Changes\">
    <input type=\"submit\" name=\"delete_$type\" value=\"Delete this type\">
            </form>
      ";
        } else {
            echo "0 results";
        }


        $conn->close();
    }
    if (isset($_POST["select_dessert"])) {
        $type = $_SESSION["stock-type"];
        $id =  $_POST['type2'];
        $_SESSION['item_id'] = $id;
        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "SELECT * FROM $type where id = $id";
        $result = $conn->query($sql);

        if ($result->num_rows > 0) {
            // output data of each row 
            echo "<form action=\"\" method=\"POST\">
             <p> Edit or Delete</p>
             
    <table style=\"margin:auto\">
    <thead>
    <th>type</th>
    <th>Price</th>
    <th>Availability</th>
    </thead>

            ";
            while ($row = $result->fetch_assoc()) {
                echo "    <td> <input value=\"" . $row["type"] . "\" type=\"text\" name=\"type\" id=\"\"></td>
                <td> 
                <input value=\"" . $row["price"] . "\" type=\"number\" name=\"price\" id=\"\"></td>
                <td><input value=\"" . $row["availability"] . "\" type=\"number\" name=\"availability\" id=\"\"></td>
                </table>";
            }
            echo "
            
    <input type=\"submit\" name=\"edit_$type\" value=\"Apply Changes\">
    <input type=\"submit\" name=\"delete_$type\" value=\"Delete this type\">
            </form>
      ";
        } else {
            echo "0 results";
        }


        $conn->close();
    }
    if (isset($_POST['edit_pizza'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $pizza_type = $_POST["type"];
        $small_Price = $_POST["small_Price"];
        $smallAvailability = $_POST["smallAvailability"];
        $medium_Price = $_POST["medium_Price"];
        $mediumAvailability = $_POST["mediumAvailability"];
        $large_Price = $_POST["large_Price"];
        $largeAvailablitiy = $_POST["largeAvailablitiy"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "UPDATE $type SET type='$pizza_type',small_Price='$small_Price',smallAvailability='$smallAvailability',medium_Price='$medium_Price',mediumAvailability='$mediumAvailability',large_Price='$large_Price',largeAvailablitiy='$largeAvailablitiy' WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "pizza updated successfully";
        } else {
            echo "Error updating record: " . $conn->error;
        }

        $conn->close();
    }
    if (isset($_POST['edit_beverage'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $beverage_type = $_POST["type"];
        $Price = $_POST["price"];
        $Availability = $_POST["availability"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "UPDATE $type SET type='$beverage_type',price='$Price',availability='$Availability' WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "beverage updated successfully";
        } else {
            echo "Error updating record: " . $conn->error;
        }

        $conn->close();
    }
    if (isset($_POST['edit_dessert'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $beverage_type = $_POST["type"];
        $Price = $_POST["price"];
        $Availability = $_POST["availability"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $sql = "UPDATE $type SET type='$beverage_type',price='$Price',availability='$Availability' WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "dessert updated successfully";
        } else {
            echo "Error updating record: " . $conn->error;
        }

        $conn->close();
    }
    if (isset($_POST['delete_pizza'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $pizza_type = $_POST["type"];
        $small_Price = $_POST["small_Price"];
        $smallAvailability = $_POST["smallAvailability"];
        $medium_Price = $_POST["medium_Price"];
        $mediumAvailability = $_POST["mediumAvailability"];
        $large_Price = $_POST["large_Price"];
        $largeAvailablitiy = $_POST["largeAvailablitiy"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // sql to delete a record
        $sql = "DELETE FROM $type WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "pizza deleted successfully";
        } else {
            echo "Error deleting record: " . $conn->error;
        }

        $conn->close();
    }
    if (isset($_POST['delete_beverage'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $beverage_type = $_POST["type"];
        $Price = $_POST["price"];
        $Availability = $_POST["availability"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // sql to delete a record
        $sql = "DELETE FROM $type WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "beverage deleted successfully";
        } else {
            echo "Error deleting record: " . $conn->error;
        }

        $conn->close();
    }
    if (isset($_POST['delete_dessert'])) {
        $type = $_SESSION["stock-type"];
        $id = $_SESSION["item_id"];
        $beverage_type = $_POST["type"];
        $Price = $_POST["price"];
        $Availability = $_POST["availability"];

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";

        // Create connection
        $conn = new mysqli($servername, $username, $password, $dbname);
        // Check connection
        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        // sql to delete a record
        $sql = "DELETE FROM $type WHERE id=$id";

        if ($conn->query($sql) === TRUE) {
            echo "dessert deleted successfully";
        } else {
            echo "Error deleting record: " . $conn->error;
        }

        $conn->close();
    }
    ?>
</body>

</html>