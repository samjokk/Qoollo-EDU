<template>
  <div class="header">
    <div class="body">
      <router-link class="logo" to="/">
        <p>QOOLLO EDU</p>
        <img alt="Logo" class="whale" src="../assets/Landing/Whale.svg" />
      </router-link>
      <div id="header-menu" class="menu">
        <router-link to="/">
          <p class="text">Главная</p>
        </router-link>
        <router-link to="/eventspage">
          <p class="text">События</p>
        </router-link>
        <router-link to="/projectspage">
          <p class="text">Проекты</p>
        </router-link>
        <router-link to="/users">
          <p class="text">Участники</p>
        </router-link>
        <div v-if="!user" @click="modal_visible = true" class="sign-in">
          <img src="../assets/Landing/SignIn.svg" alt="sign in" />
          <p>Войти</p>
        </div>
        <div v-else class="header__user-block">
          <div class="header__user-icon">
            <router-link
              class="qoollo-link"
              :to="'/participantprofile/' + user.user.url"
            >
              <img :src="user.user.photo" alt="" class="header__user-ava" />
              <span class="header__user-name">{{ user.user.name }}</span>
            </router-link>
          </div>
          <h2 @click="logout" class="header__logout">выйти</h2>
        </div>
      </div>
      <!-- <div class="wrapper3d">
        <div class="aaa">Сторона 1</div>
        <div class="bbb">Сторона 2</div>
      </div> -->
    </div>

    <div v-show="modal_visible" class="page-shadow"></div>
    <div v-show="modal_visible" class="login-modal">
      <Card />
      <div @click="modal_visible = false" class="login-modal__exit">
        <img
          src="../assets/Project/cross.png"
          alt=""
          class="login-modal__exit-img"
        />
      </div>
      <h2>Вход</h2>
      <div class="login-modal__body">
        <form action="...">
          <input
            type="text"
            placeholder="никнейм"
            class="login-modal__input"
            name="email"
          />
          <br />
          <input
            type="password"
            placeholder="пароль"
            class="login-modal__input"
            name="password"
          />
          <p id="login-error" class="login-modal__error"></p>
          <Button
            @click="login_wrap"
            class="login-modal__submit"
            :text="'Войти'"
          />
        </form>
        <router-link
          @click="modal_visible = false"
          to="/signin"
          class="qoollo-link"
          >Нет аккаунта?</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";
import Button from "@/components/Button.vue";
let backend = "http://localhost:5000";

export default {
  components: {
    Card,
    Button,
  },
  data: function () {
    return {
      modal_visible: false,
      user: false,
    };
  },
  methods: {
    async login(email, pass) {
      await fetch(backend + "/api/auth/token", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email: email, password: pass }),
      })
        .then(async (res) => {
          if (res.ok) {
            return await res.json();
          } else {
            let text = await res.json();
            throw new Error(text.errorText);
          }
        })
        .then((res) => {
          document.getElementById("login-error").innerHTML = "";
          console.log(res);
          localStorage.setItem("avatar", res.user.photo);
          this.user = res;
          $cookies.set(
            "user",
            {
              access_token: res.access_token,
              user: {
                name: res.user.name,
                surname: res.user.surname,
                roleUrl: res.user.roleUrl,
                url: res.user.url,
              },
            },
            "1D"
          );
          this.modal_visible = false;
          location.reload();
        })
        .catch((e) => {
          document.getElementById("login-error").innerHTML = e;
        });
    },
    login_wrap() {
      let email = document.getElementsByName("email")[0].value;
      let pass = document.getElementsByName("password")[0].value;
      this.login(email, pass);
    },
    logout() {
      $cookies.remove("user");
      localStorage.removeItem("avatar");
      this.user = false;
      window.location.href = "/";
    },
    printMenu(idMenuItem) {
      let menu = document.getElementById("header-menu").querySelectorAll("p");
      for (let item = 0; item < menu.length; item++) {
        if (menu[item].classList.contains("text-gradient"))
          menu[item].classList.remove("text-gradient");
      }
      if (!menu[idMenuItem].classList.contains("text-gradient"))
        menu[idMenuItem].classList.add("text-gradient");
    },
  },
  created() {
    this.user = this.$root.get_user();
  },
};
</script>

<style lang="scss">
$bg-color: #060606;
$text-color: #f8f8ff;
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
.page-shadow {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.6);
  z-index: 2;
}

.login-modal {
  position: fixed;
  z-index: 3;
  width: fit-content;
  left: auto;
  top: 25%;
  padding: 15px;
  &__error {
    color: red !important;
  }
  &__submit {
    margin: 10px 0;
  }
  &__input {
    border: none;
    margin: 10px 0;
    padding: 5px;
    background-color: $bg-color;
    border: 1px solid #424242;
    color: $text-color;
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

.header {
  height: 100px;
  display: flex;
  justify-content: center;
  &__logout {
    display: inline-block;
    vertical-align: middle;
    line-height: 40px;
    font-size: 20px !important;
    &:hover {
      cursor: pointer;
    }
  }
  &__user-block {
    vertical-align: top;
    margin-bottom: 7px;
  }
  &__user-name {
    vertical-align: middle;
    line-height: 40px;
    font-size: 20px;
  }
  &__user-icon {
    display: inline-block;
    vertical-align: top;
    text-align: center;
    margin-right: 30px;
  }
  &__user-ava {
    border-radius: 50%;
    height: 40px;
    width: 40px;
    //margin-bottom: -3px;
    margin-right: 10px;
    vertical-align: top;
  }
  .body {
    width: 1720px;
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    .logo {
      p {
        width: 200px;
      }
      img {
        position: absolute;
        top: 25px;
        left: 110px;
        z-index: -1;
      }
    }
    .menu {
      width: 1281px;
      display: flex;
      justify-content: space-between;
      align-items: flex-end;
      .sign-in {
        p {
          width: 58px;
          height: 20px;
          font-size: 18px;
        }
      }
    }
    p {
      width: 160px;
      height: 45px;
      margin: 0;
      text-align: center;
      font-family: Raleway;
      font-size: 30px;
    }
  }
  .text-gradient {
    background: $main-color;
    background-clip: text;
    -webkit-text-fill-color: transparent;
  }
}
</style>
