<html lang="en">

<head>
  <meta charset="UTF-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link rel="icon" href="/favicon/favicon.ico" />
  <link rel="stylesheet" href="/stylesheets/styles.css" />
  <link rel="preconnect" href="https://fonts.gstatic.com">
  <link href="https://fonts.googleapis.com/css2?family=Fira+Sans:wght@100&display=swap" rel="stylesheet">
  <title>sign in or sign up</title>
</head>

<body class="home-body">
  <div class="home-top-container">
    <h1>Welcome To Pizza Restaurant</h1>
    <h2>The Best Pizza is on the way.</h2>
    <h3>Now available in Mersin</h3>
    <div class="home-navigation-bar">
      <a class="home-links" href="/index.php">Home</a>
      <a class="home-links" href="">About Us</a>
      <a class="home-links" href="">Contact Us</a>
    </div>
  </div>

  <div class="overlay-color"></div>
  <!-- Main body  -->
  <div class="login-main-containter">

    <h2>Login</h2>
    <form action="/php-codes/php-login.php" name="login" method="GET">
      <label for="text">Customer Number: </label>
      <input type="text" name="customer-number" /><br><br>
      <input name="login" type="submit" value="Login">
    </form>
  </div>
  <h3>If that's your first time please register.</h3>
  <div class="signup-main-container">
    <h2>SignUp</h2>
    <div class="signup-container">
      <form name="register" method="POST" action="/php-codes/php-register.php">
        <label for="">Username: </label>
        <input name="usernameInput" type="text" />
        <br />
        <br />
        <label for="">Address: </label>
        <input name="addressInput" type="text" /><br><br>
        <input type="submit" name="register" value="Sign Up">
      </form>

    </div>
  </div>
  <div class="overlay-color3"></div>
  <!-- Main Body End -->
  <div class="home-bottom-container">
    <p>
      Made by Ammar for web programming 2 final project at Toros University
    </p>
  </div>
</body>

</html>