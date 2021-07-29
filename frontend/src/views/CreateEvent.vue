<template>
  <div class="art-wave art-wave_create-event"></div>
  <main v-if="!block">
    <h1>Создание события</h1>
      <h2>Название</h2>
      <input
        class="create-event__input"
        type="text"
        placeholder="название вашего события"
        name="title"
      />

      <h2>Теги</h2>
      <Search class="create-event__tag-search" ref="tag_search" :help="true" :active="false" :name="'tags'" :tags="tags"/>

      <h2>Описание</h2>
      <div class="redact-description">
        <span class="redact-description__mode" :class="{'redact-description__mode_chosen':!preview}" @click="preview = false">редактирование</span> | <span class="redact-description__mode" @click="load_preview" :class="{'redact-description__mode_chosen':preview}">предпросмотр</span>
        <p id="md_preview" v-show="preview"></p>
        <pre id="md_input" class="redact-description__input" v-show="!preview" name="markdown" contenteditable type="text">{{md}}</pre>
      </div>

      <h2>Начало</h2>
      <input class="create-event__input create-event__input-date" type="date" placeholder="имя" name="beginDate" />

      <h2>Окончание</h2>
      <input class="create-event__input create-event__input-date" type="date" placeholder="имя" name="endDate" />

      <p id="e-error" class="create-event__error"></p>
      <Button
        class="create-event__submit"
        @click="create_event"
        :text="'Создать'"
      />
  </main>
</template>

<script>
import marked from 'marked';
import Button from "@/components/Button.vue";
import Search from "@/components/Search.vue";
let backend = 'http://localhost:5000';

export default {
  components: {
    Button,
    Search
  },
  data: function() {
    return {
      preview: false,
      md: '*описание в формате markdown*',
      event: {is: true, name: "QoolloSummer"},
      tags: [],
      block: true
    }
  },
  methods: {
    marked,
    load_preview() {
      this.preview = true;
      let html = document.getElementById('md_input').innerHTML;
      let temp = html.replace('<br>', '\n');
      while (html != temp) {
        html = temp;
        temp = temp.replace('<br>', '\n');
      }
      document.getElementById('md_preview').innerHTML = marked(html);
    },
    async create_event() {
      let ev_name = document.getElementsByName('title')[0].value;
      let md = document.getElementsByName('markdown')[0].innerHTML;
      let b_date = document.getElementsByName('beginDate')[0].value;
      let e_date = document.getElementsByName('endDate')[0].value;
      let tags = this.$refs.tag_search.selected;

      let flag = true;
      let error = "";
      if (ev_name == '' || md == '') {
        flag = false;
        error = "название и описание не должны быть пустыми";
      }
      if (b_date == '' || e_date == '') {
        flag = false;
        error = "даты начала и конца должны быть заданы";
      } else {
        b_date = new Date(b_date);
        e_date = new Date(e_date);
        if (b_date >= e_date) {
          flag = false;
          error = "дата начала должна быть меньше даты конца";
        }
      }

      if (flag) {
        await fetch(backend + "/api/event/create", {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
          },
          body: JSON.stringify({
            "name": ev_name,
            "markdown": md,
            "tags": tags,
            "startDate": b_date,
            "endDate": e_date
          })
        }).then(async (res) => {
          let code = await res.json();
          if (code.errorCode == 0) {
            window.location.href = "/event/" + code.url;
          }
        });
      } else {
        document.getElementById('e-error').innerHTML = error;
      }
      
    }
  },
  async created() {
    this.user = this.$root.get_user();
    if (!this.user || this.user.user.roleUrl != 'organizer') {
      this.token = '';
      window.location.href = "/";
    } else {
      this.tags = await this.$root.get_tags();
      this.token = this.user.access_token;
      this.block = false;
    }
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
  &_create-event {
    background-image: url('../assets/Users/wave.png');
    background-position-x: -520px;
    background-position-y: 162px;
  }
}

.redact-description {
  &__icon {
    height: 20px;
    margin-right: 10px;
    &:hover {
      cursor: pointer;
    }
  }
  &__input {
    font-family: inherit;
    background: transparent;
    color: $txt-color;
    border-radius: 10px;
    border: 2px solid #f8f8ff;
    padding: 5px 15px 5px 5px;
    &:focus {
      outline: none;
    }
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
}

.create-event {
  padding: 15px;
  &__error {
    color: red !important;
  }
  &__tag-search {
    margin: 10px 0 20px 0;
  }
  &__submit {
    margin: 10px 0;
  }
  &__input {
    display: block;
    border: none;
    margin: 10px 0 20px 0;
    padding: 5px;
    background-color: $bg-color;
    border: 1px solid #424242;
    color: $txt-color;
    width: 200px;
    &-date {
      font-family: inherit;
    }
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
