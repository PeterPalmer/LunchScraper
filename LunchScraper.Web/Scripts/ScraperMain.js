
function ShowDay(id) {
	for (var i = 0; i < 7; i++) {
		document.getElementById("header" + i).style.display = "none";
		document.getElementById("body" + i).style.display = "none";
	}

	document.getElementById("header" + id).style.display = "block";
	document.getElementById("body" + id).style.display = "block";
}

function ToggleExceptionBody() {
	var excHeader = document.getElementById("ExceptionHeader");
	var excBody = document.getElementById("ExceptionBody");

	if (excBody.style.display == "none") {
		excBody.style.display = "block";
		excHeader.style.display = "none";
	} else {
		excBody.style.display = "none";
		excHeader.style.display = "block";
	}
}

function onSwipeLeft() {
	currentDayId++;
	if (currentDayId > 5) {
		currentDayId = 1;
	}
	ShowDay(currentDayId);
}

function onSwipeRight() {
	currentDayId--;
	if (currentDayId < 1) {
		currentDayId = 5;
	}
	ShowDay(currentDayId);
}

function detectSwipes() {
	document.addEventListener('touchstart', handleTouchStart, false);
	document.addEventListener('touchmove', handleTouchMove, false);
	document.addEventListener('touchend', handleTouchEnd, false);
}

var xDown = null;

function handleTouchStart(evt) {
	xDown = evt.touches[0].clientX;
}

function handleTouchMove(evt) {
	if (!xDown) return;

	var xDiff = getSwipeDistanceX(evt.touches);
	var offset = Math.min(Math.max(xDiff, -100), 100);
	document.getElementById("header" + currentDayId).style.marginLeft = offset + "px";
}

function handleTouchEnd(evt) {
	document.getElementById("header" + currentDayId).style.marginLeft = "0";

	var xDiff = getSwipeDistanceX(evt.changedTouches);
	xDown = null;

	if (Math.abs(xDiff) < 40) {
		return;
	}

	if (xDiff > 0) {
		onSwipeRight();
	} else {
		onSwipeLeft();
	}
}

function getSwipeDistanceX(touches) {
	if (!xDown) {
		return 0;
	}

	var xUp = touches[0].clientX;

	return xUp - xDown;
}
