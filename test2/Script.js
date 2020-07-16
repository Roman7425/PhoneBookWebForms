new Vue({
    el: "#phoneBook",
    data: {
        newId: 1,
        contacts: [],
        newName: "",
        newSurname: "",
        newPhone: "",
        newEmail: "",
        filterValue: "",
        nameError: "",
        surnameError: "",
        numberError: "",
        numberRepeatedError: "",
        emailEmptyError: "",
        emailValidError: "",
        emailError: ""
    },
    mounted() {
        this.getAllPersons(this.contacts);
    },
    computed: {
        filteredContacts: function () {
            var text = this.filterValue.toLowerCase();

            return this.contacts.filter(function (c) {
                return text.length === 0 || (c.name.toLowerCase().indexOf(text) >= 0 ||
                    c.surname.toLowerCase().indexOf(text) >= 0 ||
                    c.number.toLowerCase().indexOf(text) >= 0 ||
                    c.email.toLowerCase().indexOf(text) >= 0);
            });
        }
    },
    methods: {
        addContact: function () {
            this.nameError = "";
            this.surnameError = "";
            this.numberError = "";
            this.numberRepeatedError = "";
            this.emailEmptyError = "";
            this.emailValidError = "";
            var wasError = false;

            if (this.newName === "") {
                this.nameError = "Заполните поле \"Имя\"!";
                wasError = true;
            }
            if (this.newSurname === "") {
                this.surnameError = "Заполните поле \"Фамилия\"!";
                wasError = true;
            }
            if (this.newPhone === "" || isNaN(this.newPhone)) {
                this.numberError = "Введите номер контакта в поле \"Номер\"!"
                wasError = true;
            }
            if (this.newEmail === "") {
                this.emailEmptyError = "Введите адрес электронной почты в поле \"Email\"!";
                wasError = true;
            }
            if (!validateEmail(this.newEmail) && this.newEmail.trim() !== "") {
                this.emailValidError = "Необходимо ввести валидное значение Email!"
                wasError = true;
            }

            var temp = this.newNumber;

            if (_.find(this.contacts, function (c) {
                return c.number === temp;
            }) !== undefined) {
                this.numberRepeatedError = "Контакт с таким номером уже добавлен!";
                wasError = true;
            }

            if (wasError === true) {
                return;
            }

            this.newId = this.contacts.length + 1;

            let newPerson = { 'Name': this.newName, 'Surname': this.newSurname, 'Phone': this.newPhone, 'Email': this.newEmail };
            let uri = 'api/persons';
            $.post(uri, newPerson).done(() => {
                this.contacts = [];
                this.getAllPersons(this.contacts);
            });
                
            this.newName = "";
            this.newSurname = "";
            this.newPhone = "";
            this.newEmail = "";
        },

        deleteContact: function (contact) {
            let uri = 'api/persons/' + contact.id;
            $.ajax({
                url: uri,
                type: 'DELETE',
                success: () => {
                    this.contacts = [];
                    this.getAllPersons(this.contacts);
                }
            });
            this.newId--;
        },

        getAllPersons: function (array) {
            var uri = 'api/persons';
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        array.push({ id: item.Id, name: item.Name, surname: item.Surname, number: item.Phone, email: item.Email, check: false });
                    });
                });
        }
    }
});


function validateEmail(email) {
    var re = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
    return re.test(String(email).toLowerCase());
}