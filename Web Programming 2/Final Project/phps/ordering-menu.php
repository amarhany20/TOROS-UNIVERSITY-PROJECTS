<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="icon" href="/favicon/favicon.ico" />
  <link rel="stylesheet" href="/stylesheets/styles.css" />
  <link rel="preconnect" href="https://fonts.gstatic.com">
  <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
  <title>Select your order</title>
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
  <script>
    function onlyNumbers(num) {
      if (/[^0-9]+/.test(num.value)) {
        num.value = num.value.replace(/[^0-9]*/g, "")
      }
    }
  </script>
  <div class="ordering2-main-body">
    <form id="myform"  method="POST" action="/phps/pizza-selection-control.php">
      <div class="ordering2-all-tables">
        <?php
        if ($_SESSION['showPizza']) {
        ?>
          <div class="">
            <table class="ordering2-tables-layout ordering2-pizza-table">
              <thead>
                <th colspan="3">Type</td>
                <th>Small</td>
                <th colspan="2">Amount</td>
                <th colspan="2">Availability</td>
                <th>Medium</td>
                <th colspan="2">Medium Amount</td>
                <th colspan="2">Availability</td>
                <th>Large</td>
                <th colspan="2">Large Amount</td>
                <th colspan="2">Availability</td>
              </thead>
              <tbody>
                <?php

                $servername = "localhost";
                $username = "root";
                $password = "";
                $dbname = "pizza";
                $conn = mysqli_connect($servername, $username, $password, $dbname);
                if (!$conn) {
                  die("Connection failed: " . mysqli_connect_error());
                }
                $SELECT = mysqli_query($conn, "SELECT * FROM pizza");
                if ($SELECT != false) {
                  while ($rows = mysqli_fetch_array($SELECT)) {
                    echo "
        <tr>
            <td colspan=\"3\">" . $rows["type"] . "</td>
            <td>" . $rows["small_Price"] . " TL</td>
            <td colspan=\"2\">  <input onkeyup=\"onlyNumbers(this)\" min=\"0\" style=\"width:50px\" name=\"" . $rows["id"] . "-small\" value=\"0\" type=\"number\" name=\"\" id=\"\">  </td>
            <td colspan=\"2\">" . $rows["smallAvailability"] . "</td>
            <td>" . $rows["medium_Price"] . "  TL</td>
            <td colspan=\"2\">  <input onkeyup=\"onlyNumbers(this)\" min=\"0\" style=\"width:50px\" name=\"" . $rows["id"] . "-medium\" value=\"0\" type=\"number\" name=\"\" id=\"\"> </td>
            <td colspan=\"2\">" . $rows["mediumAvailability"] . "</td>
            <td>" . $rows["large_Price"] . "  TL</td>
            <td colspan=\"2\"> <input onkeyup=\"onlyNumbers(this)\" min=\"0\" style=\"width:50px\" name=\"" . $rows["id"] . "-large\" value=\"0\" type=\"number\" name=\"\" id=\"\"> </td>
            <td colspan=\"2\">" . $rows["largeAvailablitiy"] . "</td>
        </tr>
        ";
                  }
                } else {
                  echo "
                <tr>
                  <td colspan='3'>Something went wrong with the query</td>
                </tr>
                ";
                }
                mysqli_close($conn);
                ?>
              </tbody>
            </table>
          </div>
        <?php
        }
        if ($_SESSION['showBeverage']) {
        ?>
          <div class="ordering2-beverage-table">
            <table class="ordering2-tables-layout">
              <thead>
                <th>
                <th colspan="2">Type</td>
                <th>Price</td>
                <th>Amount</td>
                <th>Availability</td>
                </th>
              </thead>
              <tbody>
                <?php

                $servername = "localhost";
                $username = "root";
                $password = "";
                $dbname = "pizza";
                $conn = mysqli_connect($servername, $username, $password, $dbname);
                if (!$conn) {
                  die("Connection failed: " . mysqli_connect_error());
                }
                $SELECT = mysqli_query($conn, "SELECT * FROM beverage");
                if ($SELECT != false) {
                  while ($rows = mysqli_fetch_array($SELECT)) {
                    echo "
        <tr>
            <td colspan=\"3\">" . $rows["type"] . "</td>
            <td>" . $rows["price"] . " TL</td>
            <td>  <input onkeyup=\"onlyNumbers(this)\" min=\"0\" style=\"width:50px\" name=\"" . $rows["id"] . "-beverage\" value=\"0\" type=\"number\" name=\"\" id=\"\">  </td>
            <td>" . $rows["availability"] . "</td>
        </tr>
        ";
                  }
                } else {
                  echo "
                <tr>
                  <td colspan='3'>Something went wrong with the query</td>
                </tr>
                ";
                }
                mysqli_close($conn);
                ?>
            </table>
          </div>
        <?php
        }
        if ($_SESSION['showDessert']) {
        ?>
          <div class="ordering2-dessert-table">
            <table class="ordering2-tables-layout">
              <thead>
                <th>
                <th colspan="2">Type</td>
                <th>Price</td>
                <th>Amount</td>
                <th>Availability</td>
                </th>
              </thead>
              <tbody>
                <?php

                $servername = "localhost";
                $username = "root";
                $password = "";
                $dbname = "pizza";
                $conn = mysqli_connect($servername, $username, $password, $dbname);
                if (!$conn) {
                  die("Connection failed: " . mysqli_connect_error());
                }
                $SELECT = mysqli_query($conn, "SELECT * FROM dessert");
                if ($SELECT != false) {
                  while ($rows = mysqli_fetch_array($SELECT)) {
                    echo "
        <tr>
            <td colspan=\"3\">" . $rows["type"] . "</td>
            <td>" . $rows["price"] . " TL</td>
            <td>  <input onkeyup=\"onlyNumbers(this)\" min=\"0\" style=\"width:50px\" name=\"" . $rows["id"] . "-dessert\" value=\"0\" type=\"number\" name=\"\" id=\"\">  </td>
            <td>" . $rows["availability"] . "</td>
        </tr>
        ";
                  }
                } else {
                  echo "
                <tr>
                  <td colspan='3'>Something went wrong with the query</td>
                </tr>
                ";
                }
                mysqli_close($conn);
                ?>
            </table>
          </div>
        <?php
        }
        ?><br><br>
        <input name="submit" type="submit" value="submit">
      </div>
    </form>
  </div>
  <div class="ordering2-overlay-color"></div>

</body>

</html>