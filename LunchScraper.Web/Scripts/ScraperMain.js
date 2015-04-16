
function ShowDay(id) {
	for (var i = 1; i < 6; i++) {
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