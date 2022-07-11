<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="icon" href="/favicon/favicon.ico" />
  <link rel="stylesheet" href="/stylesheets/styles.css" />
  <link rel="preconnect" href="https://fonts.gstatic.com" />
  <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet" />
  <title>select region and type 🍕</title>
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
  <!-- Main Body  -->
  <div class="ordering-body">
    <form action="/php-codes/php-region-type-code.php" method="POST">
      <div class="ordering-region">
        <h1>Choose your region please</h1>



        <input value="pozcu" type="radio" name="region" />
        <label for="">Pozcu</label>
        <input value="mezitli" type="radio" name="region" />
        <label for="">Mezitli</label>
        <input value="toros-university" type="radio" name="region" />
        <label for="">Toros University</label>
        <input value="others" type="radio" name="region" />
        <label for="">Others</label>

      </div>
      <div class="ordering-type">
        <h1>Choose your type ordering</h1>

        <input type="checkbox" name="type[]" value="pizza" id="" />
        <label for="">Pizza</label>
        <input type="checkbox" name="type[]" value="beverage" id="" />
        <label for="">Beverage</label>
        <input type="checkbox" name="type[]" value="dessert" id="" />
        <label for="">Dessert</label><br><br>
        <input type="submit" name="submit" value="Submit">
      </div>
    </form>
  </div>
  <div class="ordering-overlay-color"></div>

</body>

</html>