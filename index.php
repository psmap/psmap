<?
header('Content-Type: text/html; charset=utf-8');
require './php/drive.php';
if (isset($_GET['ajax'])) {content(); exit;}
?>
<!DOCTYPE html>
<html>
<head>
<title>Вселенная<?title();?></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
<link rel="shortcut icon" type="image/x-icon" href="favicon.ico">
<link rel="stylesheet" type="text/css" href="css/style.css">
<link rel="stylesheet" type="text/css" href="css/nav.css">
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/menu.js"></script>
<script type="text/javascript" src="js/ajax.js"></script>
</head>
<body>
<div id="wrapper">
<header>
<img id="button" src="images/template/menu_button.png" alt="Меню">
<span>Вселенная</span>
</header>
<div id="mobile_menu">
<ul>    
 <li><a href="index.php?"><span>Главная</span></a></li>
 <li><a href="index.php?page=milky_way"><span>Галактика Млечный Путь</span></a></li>
 <li class="button"><span>Солнечная система</span></li>          	
 <li class="invisible"><ul>
  <li><a href="index.php?page=solar_system"><span>Описание</span></a></li>
  <li><a href="index.php?page=sun"><table><tr><td><img src="images/template/menu/sun.png" alt="Солнце"></td><td><span>Солнце</span></td></tr></table></a></li>
  <li><a href="index.php?page=planets"><table><tr><td><img src="images/template/menu/planets.png" alt="Планеты"></td><td><span>Планеты</span></td></tr></table></a></li>
  <li><a href="index.php?page=asteroids"><table><tr><td><img src="images/template/menu/asteroids.png" alt="Пояс астероидов"></td><td><span>Пояс астероидов</span></td></tr></table></a></li>
  <li><a href="index.php?page=koyper-oort"><table><tr><td><img src="images/template/menu/koyper.png" alt="Пояс Койпера и Облако Оорта"></td><td><span>Пояс Койпера и Облако Оорта</span></td></tr></table></a></li>
 </ul></li>
 <li class="button"><span>Объекты космоса</span></li>          	
 <li class="invisible"><ul>
  <li><a href="index.php?page=stars"><table><tr><td><img src="images/template/menu/stars.png" alt="stars" ></td><td><span>Звёзды</span></td></tr></table></a></li>
  <li><a href="index.php?page=exoplanets"><table><tr><td><img src="images/template/menu/exoplanets.png" alt="Экзопланеты"></td><td><span>Экзопланеты</span></td></tr></table></a></li>
  <li><a href="index.php?page=galaxy"><table><tr><td><img src="images/template/menu/galaxy.png" alt="Галактики"></td><td><span>Галактики</span></td></tr></table></a></li>
  <li><a href="index.php?page=nebula"><table><tr><td><img src="images/template/menu/nebula.png" alt="Туманности"></td><td><span>Туманности</span></td></tr></table></a></li>
  <li><a href="index.php?page=kvazar"><table><tr><td><img src="images/template/menu/kvazar.png" alt="Квазары"></td><td><span>Квазары</span></td></tr></table></a></li>
  <li><a href="index.php?page=pulsar"><table><tr><td><img src="images/template/menu/pulsar.png" alt="Пульсары"></td><td><span>Пульсары</span></td></tr></table></a></li>
 </ul></li>
 <li><a href="index.php?page=position"><span>Положение планет</span></a></li>
 <li><a href="index.php?page=moon_phases"><span>Фазы Луны</span></a></li>  
 <li><a href="index.php?page=author"><span>Об авторе</span></a></li>
</ul>
</div>
<nav>
<ul>
 <li><a href="index.php?"><span>Главная</span></a></li>
 <li><a href="index.php?page=milky_way"><span>Галактика Млечный Путь</span></a></li>
 <li class="box"><span>Солнечная система</span><ul>
  <li><a href="index.php?page=solar_system"><span>Описание</span></a></li>
  <li><a href="index.php?page=sun"><table><tr><td><img src="images/template/menu/sun.png" alt="Солнце"></td><td><span>Солнце</span></td></tr></table></a></li>
  <li><a href="index.php?page=planets"><table><tr><td><img src="images/template/menu/planets.png" alt="Планеты"></td><td><span>Планеты</span></td></tr></table></a></li>
  <li><a href="index.php?page=asteroids"><table><tr><td><img src="images/template/menu/asteroids.png" alt="Астероиды"></td><td><span>Пояс астероидов</span></td></tr></table></a></li>
  <li><a href="index.php?page=koyper-oort"><table><tr><td><img src="images/template/menu/koyper.png" alt="Пояс Койпера и Облако Оорта"></td><td><span>Пояс Койпера и Облако Оорта</span></td></tr></table></a></li>
 </ul></li>
 <li class="box"><span>Объекты космоса</span><ul>
  <li><a href="index.php?page=stars"><table><tr><td><img src="images/template/menu/stars.png" alt="Звёзды"></td><td><span>Звёзды</span></td></tr></table></a></li>
  <li><a href="index.php?page=exoplanets"><table><tr><td><img src="images/template/menu/exoplanets.png" alt="Экзопланеты"></td><td><span>Экзопланеты</span></td></tr></table></a></li>
  <li><a href="index.php?page=galaxy"><table><tr><td><img src="images/template/menu/galaxy.png" alt="Галактики"></td><td><span>Галактики</span></td></tr></table></a></li>
  <li><a href="index.php?page=nebula"><table><tr><td><img src="images/template/menu/nebula.png" alt="Туманности"></td><td><span>Туманности</span></td></tr></table></a></li>
  <li><a href="index.php?page=black_hole"><table><tr><td><img src="images/template/menu/black_hole.png" alt="Чёрные дыры"></td><td><span>Чёрные дыры</span></td></tr></table></a></li>
  <li><a href="index.php?page=kvazar"><table><tr><td><img src="images/template/menu/kvazar.png" alt="Квазары"></td><td><span>Квазары</span></td></tr></table></a></li>
  <li><a href="index.php?page=pulsar"><table><tr><td><img src="images/template/menu/pulsar.png" alt="Пульсары"></td><td><span>Пульсары</span></td></tr></table></a></li>
 </ul></li>
 <li><a href="index.php?page=position"><span>Положение планет</span></a></li>
 <li><a href="index.php?page=moon_phases"><span>Фазы Луны</span></a></li>
 <li><a href="index.php?page=author"><span>Об авторе</span></a></li>
</ul>
</nav>
<div id="content"><?content();?></div>
<footer>
<img id="counter" src="images/template/counter.png" alt="IT Counter">
<img src="images/template/html5logo.png" alt="HTML5" title="HTML5">
<img src="images/template/vcss.gif" alt="Правильный CSS!" title="Правильный CSS!">
<span>Copyright &copy; Universe | Design by Gorenkov Dmitry, 2016</span>
</footer>
</div>
</body>
</html>