
function ShowDay(id, isPreviousDay) {
	for (var i = 0; i < 7; i++) {
		document.getElementById("header" + i).style.display = "none";
		document.getElementById("body" + i).style.display = "none";
	}

	var header = document.getElementById("header" + id);
	var body = document.getElementById("body" + id);

	header.classList.remove('slideRight', 'slideLeft');
	body.classList.remove('slideRight', 'slideLeft');

	header.style.display = "block";
	body.style.display = "block";

	if (isPreviousDay) {
		header.classList.add('slideRight');
		body.classList.add('slideRight');
	}
	else {
		header.classList.add('slideLeft');
		body.classList.add('slideLeft');
	}
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
	ShowDay(currentDayId, false);
}

function onSwipeRight() {
	currentDayId--;
	if (currentDayId < 1) {
		currentDayId = 5;
	}
	ShowDay(currentDayId, true);
}

function detectSwipes() {
	document.addEventListener('touchstart', handleTouchStart, false);
	document.addEventListener('touchmove', handleTouchMove, false);
	document.addEventListener('touchend', handleTouchEnd, false);
}


var xDown = null;
var yDown = null;
var startTime;
var elapsedTime;
var swipeDirection;
var horizontal = 1;
var vertical = 2;

function handleTouchStart(evt) {
	xDown = evt.touches[0].pageX;
	yDown = evt.touches[0].pageY;
	startTime = new Date().getTime();
	swipeDirection = null;
}

function handleTouchMove(evt) {
	if (!xDown) return;

	if (!swipeDirection) {
		if (Math.abs(getSwipeDistanceX(evt.touches)) > Math.abs(getSwipeDistanceY(evt.touches))) {
			swipeDirection = horizontal;
		}
		else {
			swipeDirection = vertical;
		}
	}

	if (swipeDirection == vertical) return;

	var xDiff = getSwipeDistanceX(evt.touches);
	var offset = Math.min(Math.max(xDiff, -300), 300);
	document.getElementsByTagName("body")[0].style.marginLeft = offset + "px";
	document.getElementsByTagName("body")[0].style.marginRight = (-1 * offset) + "px";
}

function handleTouchEnd(evt) {
	var xDiff = getSwipeDistanceX(evt.changedTouches);
	xDown = null;
	yDown = null;

	document.getElementsByTagName("body")[0].style.marginLeft = "0";
	document.getElementsByTagName("body")[0].style.marginRight = "0";

	elapsedTime = new Date().getTime() - startTime;
	if (elapsedTime <= 100) return;

	if (swipeDirection == vertical) {
		return;
	}

	if (Math.abs(xDiff) < 100) {
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

	var xUp = touches[0].pageX;

	return xUp - xDown;
}

function getSwipeDistanceY(touches) {
	if (!yDown) {
		return 0;
	}

	var yUp = touches[0].pageY;

	return yUp - yDown;
}