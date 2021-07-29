<template>
  <Header ref="header" />
  <router-view />
</template>

<script>
import Header from "@/components/Header.vue";
let backend = "http://localhost:5000";

export default {
  components: {
    Header,
  },
  methods: {
    async get_tags() {
      const res = await fetch(backend + "/api/tag", {
        method: "GET",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      });
      return res.json();
    },
    get_user() {
      let user = $cookies.get("user");
      if (user) user.user.photo = localStorage.getItem("avatar");
      return user;
    },
  },
};
</script>

<style lang="scss">
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$bg-color: #060606;
$text-color: #f8f8ff;

body {
  overflow-y: auto;
  overflow-x: hidden;
  min-height: 100vh;
  &::-webkit-scrollbar {
    width: 17px;
    border-radius: 25px;
    background: #1f1f1f;
  }
  &::-webkit-scrollbar-thumb {
    width: 10px;
    background: -webkit-linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
    box-shadow: inset 2px 10000px 2px rgb(20, 20, 20, 1);
    border: solid 2px transparent;
    background-origin: border-box;
    border-radius: 25px;
  }
  margin: 0;
  background-color: $bg-color;
  position: relative;
  color: $text-color;
  #app {
    margin: 0;
    font-family: OpenSans;
    font-style: normal;
    font-weight: normal;
    font-size: 16px;
    p {
      color: $text-color;
    }
    span {
      color: $text-color;
    }
    a {
      color: inherit;
      text-decoration: none;
    }
    h1,
    h2,
    .qoollo-header {
      font-size: 30px;
      margin: 0 0;
      font-family: Raleway;
      font-style: normal;
      font-weight: normal;
    }
    h2 {
      font-size: 24px;
    }
  }
}

.art-wave {
  background-repeat: no-repeat;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: -1;
}

.qoollo-link {
  background: $main-color;
  background-clip: text;
  -webkit-text-fill-color: transparent;
  text-decoration: none;
}

@font-face {
  font-family: "Raleway";
  src: url("./fonts/Raleway-Regular.ttf") format("truetype");
  font-style: normal;
  font-weight: normal;
}

@font-face {
  font-family: "OpenSans";
  src: url("./fonts/OpenSans-Regular.ttf") format("truetype");
  font-style: normal;
  font-weight: normal;
}
</style>
