<template>
  <div class="art-wave art-wave_create-project"></div>
  <main v-if="!block">
    <h1>Создание проекта <span v-if="event.is">| <router-link class="qoollo-link" :to="'/event/' + event.id"> {{event.name}}</router-link></span></h1>
      <h2>Название</h2>
      <input
        class="create-project__input"
        type="text"
        placeholder="название вашего проекта"
        name="title"
      />

      <h2>Теги</h2>
      <Search class="create-project__tag-search" ref="tag_search" :help="true" :active="false" :name="'tags'" :tags="tags"/>

      <h2>Описание</h2>
      <div class="redact-description">
        <span class="redact-description__mode" :class="{'redact-description__mode_chosen':!preview}" @click="preview = false">редактирование</span> | <span class="redact-description__mode" @click="load_preview" :class="{'redact-description__mode_chosen':preview}">предпросмотр</span>
        <p id="md_preview" v-show="preview"></p>
        <pre id="md_input" class="redact-description__input" v-show="!preview" name="markdown" contenteditable type="text">{{md}}</pre>
      </div>

      <h2>Медиа</h2>
      <div class="create-project__media">
        <div v-for="(s, ix) in sources" @click="toggle(ix)" 
          class="create-project__media-handler" :class="{'create-project__media-handler_chosen':chosen.includes(ix)}">
          <img class="create-project__media-img" :src="s.link" alt="">
        </div>
        <div class="button create-project__media-add">
          <input @change="add_media" id="loader" class="create-project__input create-project__input-file" type="file" placeholder="имя" name="media"/>
          <img src="../assets/CreateProject/plus.png" alt="" class="create-project__btn-img">
        </div>
        <div class="button button create-project__media-remove" @click="remove">
          <img src="../assets/CreateProject/cross.png" alt="" class="create-project__btn-img">
        </div>
      </div>
      <p id="p-error" class="create-project__error"></p>
      <Button
        class="create-project__submit"
        @click="create_project"
        :text="'Создать'"
      />
  </main>
</template>

<script>
import marked from 'marked';
import Button from "@/components/Button.vue";
import Search from "@/components/Search.vue";
let backend = 'http://localhost:5000';

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

export default {
  components: {
    Button,
    Search
  },
  props: {
    eventId: Number,
    eventName: String
  },
  data: function() {
    return {
      preview: false,
      md: '*описание в формате markdown*',
      event: {is: false, id: null, name: ''},
      sources: [],
      block: true,
      chosen: []
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
    async add_media() {
      let f = document.getElementById('loader').files[0];
      const result = await toBase64(f).catch(e => Error(e));
      this.sources.push({link: result, type: 0});
    },
    async create_project() {
      let proj_name = document.getElementsByName('title')[0].value;
      let md = document.getElementsByName('markdown')[0].innerHTML;
      if (md.slice(md.length - 4) == '<br>')
        md = md.slice(0, md.length - 4);
      let tags = this.$refs.tag_search.selected;

      let flag = true;
      let error = "";
      if (proj_name == '' || md == '') {
        flag = false;
        error = "название и описание не должны быть пустыми";
      }

      if (flag) {
        await fetch(backend + "/api/project/create", {
          method: 'POST',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + this.token
          },
          body: JSON.stringify({
            "name": proj_name,
            "markdown": md,
            "tags": tags,
            eventId: this.event.id,
            "mediaContent": this.sources
          })
        }).then(async (res) => {
          let code = await res.json();
          if (code.errorCode == 0) {
            window.location.href = "/project/" + code.url;
          }
        });
      } else {
        document.getElementById('p-error').innerHTML = error;
      }
    },
    toggle(ix) {
      let ind = this.chosen.indexOf(ix);
      if (ind != -1)
        this.chosen.splice(ind, 1);
      else
        this.chosen.push(ix);
    },
    remove() {
      this.chosen.sort().reverse().forEach(e => {
        this.sources.splice(e, 1);
      });
      this.chosen.splice(0);
    }
  },
  async created() {
    if (this.$route.params.eventId) {
      this.event.is = true;
      this.event.id = this.$route.params.eventId;
      this.event.name = this.$route.params.eventName;
    }
    
    this.user = this.$root.get_user();
    if (!this.user || this.user.user.roleUrl != 'profile') {
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
  &_create-project {
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

.create-project {
  padding: 15px;
  width: 610px;
  &__error {
    color: red !important;
  }
  &__tag-search {
    margin: 10px 0 20px 0;
  }
  &__submit {
    margin: 10px 0;
  }
  &__btn-img {
    vertical-align: middle;
    height: 100%;
  }
  &__media {
    padding-top: 10px;
    margin-bottom: 20px;
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    max-width: 618px;
    &-add, &-remove {
      position: relative;
      height: 20px;
      width: 20px;
      padding: 5px 10px 10px 10px;
    }
    &-img {
      height: 100%;
      margin: 0 auto;
    }
    &-handler {
      width: 300px;
      height: 169px;
      padding: 2px;
      background-color: #000;
      text-align: center;
      &_chosen {
        padding: 0;
        border: solid 2px transparent;
        background-origin: border-box;
        background-image: $main-color;
        box-shadow: inset 2px 1000px 1px black;
      }
      &:hover {
        cursor: pointer;
      }
    }
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
    &:focus {
      outline: none;
      border-bottom: solid 3px transparent;
      background-origin: border-box;
      background-image: $main-color;
      box-shadow: inset 2px 1000px 1px $bg-color;
      padding-bottom: 3px;
    }
    
    &-file {
      position: absolute;
      left: 0;
      top: 0;
      opacity: 0;
      width: 100%;
      height: 100%;
      margin: 0;
      padding: 0;
      z-index: 2;
      &:hover {
        cursor: pointer;
      }
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
