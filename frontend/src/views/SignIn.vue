<template>
  <div class="art-wave art-wave_signin"></div>
  <main>
    <h2 class="signin__header">Регистрация</h2>
    <div class="signin__modes">
      <span class="signin__mode" :class="{'signin__mode_chosen':type==0}" @click="type=0">разработчик</span> | <span class="signin__mode" @click="type=1" :class="{'signin__mode_chosen':type==1}">организатор</span>
    </div>
    <div v-show="type == 0">
      <input class="signin__input" type="text" placeholder="дата рождения" name="dev-birthdate"
        onfocus="(this.type='date')" onblur="(this.type='text')"/>
      <input class="signin__input" type="text" placeholder="имя" name="dev-name" />
      <input class="signin__input" type="text" placeholder="фамилия" name="dev-surname"/>
      <input class="signin__input" type="email" placeholder="почта" name="dev-email"/>
      <input class="signin__input" type="text" placeholder="никнейм" name="dev-nickname"/>
      <input class="signin__input" type="password" placeholder="пароль" name="dev-password"/>
      <input class="signin__input" type="password" placeholder="повторите пароль" name="dev-re-password"/>
      <p id="dev-error" class="signin__dev-error"></p>
      <Button
        class="signin__submit" @click="reg_dev"
        :text="'Создать аккаунт'"
      />
    </div>
    <div v-show="type == 1">
      <input class="signin__input" type="text" placeholder="название организации" name="comp_name" />
      <input class="signin__input" type="email" placeholder="почта" name="comp_email"/>
      <input class="signin__input" type="password" placeholder="пароль" name="comp_password"/>
      <input class="signin__input" type="password" placeholder="повторите пароль" name="comp_re-password"/>
      <Button
        class="signin__submit"
        onclick="javascript:this.parentNode.submit();"
        :text="'Создать аккаунт'"
      />
    </div>
  </main>
</template>

<script>
import Button from "@/components/Button.vue";
let backend = 'http://localhost:5000';

export default {
  components: {
    Button,
  },
  data: function() {
    return {
      type: 0
    }
  },
  methods: {
    async reg_dev() {
      let email = document.getElementsByName('dev-email')[0].value;
      let firstname = document.getElementsByName('dev-name')[0].value;
      let surname = document.getElementsByName('dev-surname')[0].value;
      let bi_da = document.getElementsByName('dev-birthdate')[0].value;
      let nickname = document.getElementsByName('dev-nickname')[0].value;
      let password = document.getElementsByName('dev-password')[0].value;
      let re_password = document.getElementsByName('dev-re-password')[0].value;
      let flag = true;
      let error = "";
      if (password != re_password) {
        flag = false;
        error = "пароли должны совпадать";
      }
      if (email == '' || firstname == '' || surname == '' || bi_da == '' || nickname == '' || password == '') {
        flag = false;
        error = "все поля должны быть заполнены";
      }

      if (flag) {
        await fetch(backend + "/api/profile/create", {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
          },
          body: JSON.stringify({
            email: email,
            name: firstname,
            surname: surname,
            birthDate: new Date(bi_da),
            nickname: nickname,
            password: password
            })
        }).then(async (res) => {
          let code = await res.json();
          switch (code.errorCode) {
            case 0: {
              this.$root.$refs.header.login(email, password);
              break;
            }
            case 1: {
              document.getElementById('dev-error').innerHTML = 'Эта почта уже занята';
              break;
            }
            case 2: {
              document.getElementById('dev-error').innerHTML = 'Этот никнейм уже занят';
            }
          }
        })
      } else {
        document.getElementById('dev-error').innerHTML = error;
      }
    }
  },
  beforeCreate() {
    this.user = this.$root.get_user();
    if (this.user) {
      this.token = this.user.access_token;
      window.location.href = "/";
    } else
      this.token = ''
  }
};
</script>

<style lang="scss">
$bg-color: #060606;
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$card-border: linear-gradient(
  90deg,
  rgba(73, 73, 73, 1) 0%,
  rgba(0, 0, 0, 0) 100%
);
$card-bg: rgba(39, 39, 47, 0.4);
$txt-color: #f8f8ff;

.i-b {
  display: inline-block;
}

.art-wave {
  &_signin {
    background-image: url('../assets/Users/wave.png');
    background-position-x: -420px;
    background-position-y: 112px;
  }
}

.signin {
  padding: 15px;
  &__dev-error {
    color: red !important;
  }
  &__header {
    margin-bottom: 15px !important;
  }
  &__modes {
    margin-bottom: 15px;
  }
  &__mode {
    font-size: 20px;
    &:hover {
      cursor: pointer;
    }
    &_chosen {
      font-weight: bold;
      text-decoration: underline;
    }
  }
  &__submit {
    margin: 10px 0;
  }
  &__input {
    display: block;
    border: none;
    margin: 10px 0;
    padding: 5px;
    background-color: $bg-color;
    border: 1px solid #424242;
    color: $txt-color;
    width: 200px;
    &:focus {
      outline: none;
      border-bottom: solid 3px transparent;
      background-origin: border-box;
      background-image: $main-color;
      box-shadow: inset 2px 1000px 1px $bg-color;
      padding-bottom: 3px;
    }
  }
  &__exit {
    position: absolute;
    top: 5px;
    right: 5px;
    &:hover {
      cursor: pointer;
    }
    &-img {
      width: 30px;
      height: 30px;
    }
  }
  &__body {
    padding-top: 20px;
  }
}

.qoollo-link {
  background: $main-color;
  background-clip: text;
  -webkit-text-fill-color: transparent;
  text-decoration: none;
}

main {
  width: fit-content;
  margin: 0 auto;
  //height: 100%;
  color: $txt-color;
  padding: 50px 0;
}
</style>
