var func = (function () {

    window.testfunction = {
        helloWorld: function () {
            return alert('Hello World');
        },
        inputName: function (text) {
            return prompt(text, 'input Name');
        }
    };
});

func();