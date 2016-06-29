<?
function title(){
	if (isset($_GET['page'])) $page = $_GET['page'];
	if (isset($page)){
		echo ' | '; 
		switch($page){
			case 'milky_way': echo 'Галактика Млечный Путь'; break;
			case 'solar_system': echo 'Солнечная система'; break;
			case 'planets': echo 'Планеты Солнечной системы'; break;
			case 'asteroids': echo 'Пояс астероидов'; break;
			case 'koyper-oort': echo 'Пояс Койпера и Облако Оорта'; break;
			case 'position': echo 'Расчёт положения планет'; break;
			case 'moon_phases': echo 'Фазы Луны'; break;
			case 'exoplanets': echo 'Экзопланеты'; break;
			case 'stars': echo 'Звёзды'; break;
			case 'galaxy': echo 'Галактики'; break;
			case 'nebula': echo 'Туманности'; break;
			case 'black_hole': echo 'Чёрные дыры'; break;
			case 'kvazar': echo 'Квазары'; break;
			case 'pulsar': echo 'Пульсары'; break;
			case 'mercury': echo 'Меркурий'; break;
			case 'venus': echo 'Венера'; break;
			case 'earth': echo 'Земля'; break;
			case 'mars': echo 'Марс'; break;
			case 'jupiter': echo 'Юпитер'; break;
			case 'saturn': echo 'Сатурн'; break;
			case 'uranus': echo 'Уран'; break;
			case 'neptune': echo 'Нептун'; break;
			case 'sun': echo 'Солнце'; break;
			case 'author': echo 'Об авторе'; break;
			default: echo 'Ошибка'; break;
		}
	}
}

function content(){
	if (isset($_GET['page'])) $page = $_GET['page'];
	if (isset($page)) {
		switch($page){
			case 'milky_way': include './content/milky_way.php'; break; 
			case 'solar_system': include './content/solar_system.php'; break;
			case 'planets': include './content/planets.php'; break;
			case 'asteroids': include './content/asteroids.php'; break;
			case 'koyper-oort': include './content/koyper-oort.php'; break;
			case 'position': include './content/position.php'; break;
			case 'moon_phases': include './content/moon_phases.php'; break;
			case 'exoplanets': include './content/exoplanets.php'; break;
			case 'stars': include './content/stars.php'; break;
			case 'galaxy': include './content/galaxy.php'; break;
			case 'nebula': include './content/nebula.php'; break;
			case 'black_hole': include './content/black_hole.php'; break;
			case 'kvazar': include './content/kvazar.php'; break;
			case 'pulsar': include './content/pulsar.php'; break;
			case 'mercury': include './content/mercury.php'; break;
			case 'venus': include './content/venus.php'; break;
			case 'earth': include './content/earth.php'; break;
			case 'mars': include './content/mars.php'; break;
			case 'jupiter': include './content/jupiter.php'; break;
			case 'saturn': include './content/saturn.php'; break;
			case 'uranus': include './content/uranus.php'; break;
			case 'neptune': include './content/neptune.php'; break;
			case 'sun': include './content/sun.php'; break;
			case 'author': include './content/author.php'; break;
			default: include './content/error.php'; break;
		}
	}
	else include './content/main.php';
}
?>