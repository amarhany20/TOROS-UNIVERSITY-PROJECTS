<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="icon" href="/favicon/favicon.ico" />
    <link rel="stylesheet" href="/stylesheets/styles.css" />
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
    <title>Confirmation</title>
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
            $username = $_SESSION['username'];
            $customerNumber = $_SESSION['customer-number'];
            echo "<h3 class=\"home-links\" style=\"display: inline; text-align: right; position: absolute; top: 145px; right: 10px;\" >$username - $customerNumber</h3>";
            ?>
        </div>
    </div>

    <div class="overlay-color"></div>
    <!-- main body  -->
    <?php
    if (isset($_POST['submit'])) {

        $servername = "localhost";
        $username = "root";
        $password = "";
        $dbname = "pizza";
        $conn = mysqli_connect($servername, $username, $password, $dbname);
        if (!$conn) {
            die("Connection failed: " . mysqli_connect_error());
        }
        $errorCheck = false;
        //checking selection
        $pizzaSelection = false;
        $beverageSelection = false;
        $dessertSelection = false;

        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-small"] != "0" || $_POST[$rows["id"] . "-medium"] != "0" || $_POST[$rows["id"] . "-large"] != "0") {
                        // echo "pizza selection detected <br>";
                        $pizzaSelection = true;
                        break;
                    }
                }
                if (!$pizzaSelection) {
                    echo "No Pizza Selection detected <br> <br>";
                    $errorCheck = true;
                }
            }
        }
        if ($_SESSION['showBeverage']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM beverage");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-beverage"] != "0") {
                        // echo "beverage selection detected <br> ";
                        $beverageSelection = true;
                        break;
                    }
                }
                if (!$beverageSelection) {
                    echo "No beverage Selection detected <br> <br>";
                    $errorCheck = true;
                }
            }
        }
        if ($_SESSION['showDessert']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM dessert");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-dessert"] != "0") {
                        // echo "dessert selection detected <br>";
                        $dessertSelection = true;
                        break;
                    }
                }
                if (!$dessertSelection) {
                    echo "No dessert Selection detected <br> <br>";
                    $errorCheck = true;
                }
            }
        }
        //Checking availability 
        $smallPizzaAmounts = array();
        $mediumPizzaAmounts = array();
        $largePizzaAmounts = array();
        $beverageAmounts = array();
        $dessertAmounts = array();

        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-small"] > $rows["smallAvailability"]) {
                        echo "the amount you entered for small " . $rows["type"] . " is not available. Please decrease the amount entered or it's out of stock. <br> <br> ";
                        $errorCheck = true;
                    }
                    $smallPizzaAmounts[$rows["id"]] = $_POST[$rows["id"] . "-small"];
                }
            }
        }
        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-medium"] > $rows["mediumAvailability"]) {
                        echo "the amount you entered for medium " . $rows["type"] . " is not available. Please decrease the amount entered or it's out of stock. <br> <br>";
                        $errorCheck = true;
                    }
                    $mediumPizzaAmounts[$rows["id"]] = $_POST[$rows["id"] . "-medium"];
                }
            }
        }
        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-large"] > $rows["largeAvailablitiy"]) {
                        echo "the amount you entered for large " . $rows["type"] . " is not available. Please decrease the amount entered or it's out of stock. <br> <br>";
                        $errorCheck = true;
                    }
                    $largePizzaAmounts[$rows["id"]] = $_POST[$rows["id"] . "-large"];
                }
            }
        }
        if ($_SESSION['showBeverage']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM beverage");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-beverage"] > $rows["availability"]) {
                        echo "the amount you entered for " . $rows["type"] . " is not available. Please decrease the amount entered or it's out of stock. <br> <br>";
                        $errorCheck = true;
                    }
                    $beverageAmounts[$rows["id"]] = $_POST[$rows["id"] . "-beverage"];
                }
            }
        }
        if ($_SESSION['showDessert']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM dessert");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    if ($_POST[$rows["id"] . "-dessert"] > $rows["availability"]) {
                        echo "the amount you entered for " . $rows["type"] . " is not available. Please decrease the amount entered or it's out of stock. <br> <br>";
                        $errorCheck = true;
                    }
                    $dessertAmounts[$rows["id"]] = $_POST[$rows["id"] . "-dessert"];
                }
            }
        }
        if ($errorCheck) {
            echo "<button value=\"Click here to go back\" onclick=\"goBack()\">Go Back</button>

            <script>
            function goBack() {
              window.history.back();
            }
            </script>
            <div class=\"ordering2-overlay-color\"></div>";
            return;
        }
        //calculating price
        $totalPizza = 0;
        $totalBeverage = 0;
        $totalDessert = 0;
        $totalPrice = 0;

        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    $totalPizza +=  ($_POST[$rows["id"] . "-small"] * $rows["small_Price"]);
                }
            }
        }
        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    $totalPizza +=  ($_POST[$rows["id"] . "-medium"] * $rows["medium_Price"]);
                }
            }
        }
        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    $totalPizza +=  ($_POST[$rows["id"] . "-large"] * $rows["large_Price"]);
                }
            }
        }
        if ($_SESSION['showBeverage']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM beverage");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    $totalBeverage +=  ($_POST[$rows["id"] . "-beverage"] * $rows["price"]);
                }
            }
        }
        if ($_SESSION['showDessert']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM dessert");
            if ($SELECT != false) {
                while ($rows = mysqli_fetch_array($SELECT)) {
                    $totalDessert +=  ($_POST[$rows["id"] . "-dessert"] * $rows["price"]);
                }
            }
        }
        $totalPrice = $totalPizza + $totalBeverage + $totalDessert;

        echo "
        <form method=\"POST\" action=\"\">
        <table style=\"margin:auto;border-spacing: 10px;\">
    <tr>
    <td>Total Pizza Price: </td>
    <td>$totalPizza TL</td>
    </tr>
    <tr>
    <td>Total Beverage Price: </td>
    <td>$totalBeverage TL</td>
    </tr>
    <tr>
    <td>Total Dessert Price: </td>
    <td>$totalDessert TL</td>
    </tr>
    <tr>
    <td>Total Price: </td>
    <td>$totalPrice TL</td>
    </tr>
  </table>
  <input value=\"Complete Purchase\" name=\"submit2\" type=\"submit\">
  
  </form>
        ";

        $_SESSION['smallPizzaAmountsArray'] = $smallPizzaAmounts;
        $_SESSION['mediumPizzaAmountsArray'] = $mediumPizzaAmounts;
        $_SESSION['largePizzaAmountsArray'] = $largePizzaAmounts;
        $_SESSION['beverageAmountsArray'] = $beverageAmounts;
        $_SESSION['dessertAmountsArray'] = $dessertAmounts;
        $_SESSION["totalPizza"] = $totalPizza;
        $_SESSION["totalBeverage"] = $totalBeverage;
        $_SESSION["totalDessert"] = $totalDessert;
        $_SESSION["totalPrice"] = $totalPrice;
    }
    ?>
    <div class="ordering2-overlay-color"></div>

    <?php
    if (isset($_POST["submit2"])) {


        $smallPizzaAmounts = $_SESSION['smallPizzaAmountsArray'];
        $mediumPizzaAmounts = $_SESSION['mediumPizzaAmountsArray'];
        $largePizzaAmounts = $_SESSION['largePizzaAmountsArray'];
        $beverageAmounts = $_SESSION['beverageAmountsArray'];
        $dessertAmounts = $_SESSION['dessertAmountsArray'];
        $totalPizza = $_SESSION['totalPizza'];
        $totalBeverage = $_SESSION['totalBeverage'];
        $totalDessert = $_SESSION['totalDessert'];
        $totalPrice = $_SESSION['totalPrice'];

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

        if ($_SESSION['showPizza']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
            while ($rows = mysqli_fetch_array($SELECT)) {
                $sql = "UPDATE pizza SET smallAvailability= smallAvailability-" . $smallPizzaAmounts[$rows["id"]] . " WHERE id=" . $rows['id'];
                if ($conn->query($sql) === TRUE) {
                    echo "Record updated successfully";
                } else {
                    echo "Error updating record: " . $conn->error;
                }
                $sql = "UPDATE pizza SET mediumAvailability= mediumAvailability-" . $mediumPizzaAmounts[$rows["id"]] . " WHERE id=" . $rows['id'];
                if ($conn->query($sql) === TRUE) {
                    echo "Record updated successfully";
                } else {
                    echo "Error updating record: " . $conn->error;
                }
                $sql = "UPDATE pizza SET largeAvailablitiy= largeAvailablitiy-" . $largePizzaAmounts[$rows["id"]] . " WHERE  id=" . $rows['id'];
                if ($conn->query($sql) === TRUE) {
                    echo "Record updated successfully";
                } else {
                    echo "Error updating record: " . $conn->error;
                }
            }
        }
        if ($_SESSION['showBeverage']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM beverage");
            while ($rows = mysqli_fetch_array($SELECT)) {
                $sql = "UPDATE beverage SET availability= availability-" . $beverageAmounts[$rows["id"]] . " WHERE id=" . $rows['id'];
                if ($conn->query($sql) === TRUE) {
                    echo "Record updated successfully";
                } else {
                    echo "Error updating record: " . $conn->error;
                }
            }
        }

        if ($_SESSION['showDessert']) {
            $SELECT = mysqli_query($conn, "SELECT * FROM dessert");
            while ($rows = mysqli_fetch_array($SELECT)) {
                $sql = "UPDATE dessert SET availability= availability-" . $dessertAmounts[$rows["id"]] . " WHERE id=" . $rows['id'];
                if ($conn->query($sql) === TRUE) {
                    echo "Record updated successfully";
                } else {
                    echo "Error updating record: " . $conn->error;
                }
            }
        }

        $discount = 0;

        if ($totalPrice == 50) {
            $discount = 50 * 0.10;
        } else if ($totalPrice > 50 && $totalPrice <= 70) {
            $discount = $totalPrice * 0.15;
        } else if ($totalPrice > 70) {
            $discount = $totalPrice * 0.20;
        }

        $currentdate = date("m/d/Y");

        $sql = "INSERT INTO orders (customer_number, total_price, discount,order_date) VALUES ('$customerNumber','$totalPrice','$discount',STR_TO_DATE('$currentdate', '%m/%d/%Y')) ";

        if ($conn->query($sql) === TRUE) {
            echo "New record created successfully";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
        $_SESSION["totalPrice"] = $totalPrice;
        $_SESSION["discount"] = $discount;
        $_SESSION["currentdate"] = $currentdate;
        header("Location: /phps/bill.php");
    }

    $conn->close();
    ?>

</body>

</html>