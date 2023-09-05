window.PlaySound = function (name) {
	let a = document.getElementById(name);
	a.currentTime = 0;
	a.play();
}