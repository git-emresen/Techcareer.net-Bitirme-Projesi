const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});

/*  Sign up   */

var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirm_password");

function validatePassword() {

	confirm_password.setCustomValidity(password.value != confirm_password.value ? "Passwords Don't Match" : '');
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;

//////////////////////