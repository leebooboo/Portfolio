
//alertify 1.13.1 세팅 자바스크립트

//테마를 bootstrap 으로 설정
alertify.defaults.transition = "slide";
alertify.defaults.theme.ok = "btn btn-primary";
alertify.defaults.theme.cancel = "btn btn-danger";
alertify.defaults.theme.input = "form-control";

alertify.defaults.title = "보드몰";

alertify.alert().setting({
	"label": "확인"
});

alertify.confirm().setting({
	"labels": {
		ok: "예",
		cancel: "아니오"
	}
});        
