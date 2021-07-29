<template>
  <div
    v-show="
      visible.watch_requests ||
      visible.users ||
      visible.news ||
      visible.add_news ||
      visible.send_request
    "
    class="page-shadow"
  ></div>
  <div v-show="visible.users" class="modal">
    <Card />
    <div @click="close_modal('users')" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Участники</h2>
    <div class="modal__body modal__body_user-flex">
      <router-link
        v-for="member in project.members"
        :to="'/participantprofile/' + member.nickname"
      >
        <img
          :src="member.photo"
          alt="a"
          class="modal__user-avatar"
          :title="member.name + ' ' + member.surname"
        />
      </router-link>
    </div>
  </div>

  <div v-show="visible.news" class="modal">
    <Card />
    <div @click="close_modal('news')" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Новости</h2>
    <div v-if="ready" class="modal__body modal__body_scroll">
      <NewsCard
        class="modal__news-card"
        v-for="n in project.news"
        :proj_id="id"
        :title="n.name"
        :text="n.content"
        :id="n.id"
        :date="new Date(n.publishDate)"
      />
    </div>
  </div>

  <div v-show="visible.send_request" class="modal">
    <Card />
    <div @click="visible.send_request = false" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Заявка на вступление</h2>
    <div class="modal__body">
      <textarea
        id="request-contents"
        class="modal__textarea"
        name=""
        cols="30"
        rows="3"
      ></textarea>
      <Button v-on:click="send_request" :text="'Отправить'" />
    </div>
  </div>

  <div v-if="visible.add_news && owner_cond" class="modal">
    <Card />
    <div @click="visible.add_news = false" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Создание новости</h2>
    <div class="modal__body">
      Заголовок
      <input class="modal__input" type="text" id="news-title" />
      Содержание
      <textarea
        id="news-contents"
        class="modal__textarea"
        name=""
        cols="30"
        rows="3"
      ></textarea>
      <Button v-on:click="publish" :text="'Опубликовать'" />
    </div>
  </div>

  <div v-if="visible.watch_requests && owner_cond" class="modal">
    <Card />
    <div @click="close_modal('watch_requests')" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Просмотр заявок</h2>
    <div class="modal__body modal__body_scroll">
      <div v-for="(req, ix) in requests" class="modal__request">
        <Comment
          :dto="{
            comment: req.request.message,
            user: {
              name: req.user.name,
              surname: req.user.surname,
              photo: req.user.photo,
              nickname: req.user.nickname,
            },
          }"
        />
        <Button
          class="modal__req-btn"
          v-on:click="accept_request(req.request.developerId, ix)"
          :text="'Принять'"
        />
        <Button
          class="modal__req-btn"
          v-on:click="decline_request(req.request.developerId, ix)"
          :text="'Отклонить'"
        />
      </div>
    </div>
  </div>

  <div class="art-wave art-wave_project"></div>
  <main>
    <div class="project-header">
      <h1>
        <span v-if="!redact.title">{{ title }}</span>
        <span
          v-else
          type="text"
          id="title_input"
          class="qoollo-header project-header__title-input"
          contenteditable
          >{{ title }}</span
        >
        <img
          @click="redact.title = true"
          v-if="owner_cond && !redact.title"
          src="../assets/Project/redact.png"
          alt=""
          class="project-header__icon"
        />
        <span v-else-if="owner_cond && redact.title">
          <img
            @click="save_title"
            src="../assets/Project/ok.png"
            alt=""
            class="project-header__icon"
          />
          <img
            @click="redact.title = false"
            src="../assets/Project/cross.png"
            alt=""
            class="project-header__icon"
          />
        </span>
        |
        <router-link
          class="qoollo-link"
          :to="'/participantprofile/' + project.owner.nickname"
          ><img
            :src="project.owner.photo"
            alt=""
            class="project-header__avatar"
          />
          {{
            project.owner.name + " " + project.owner.surname + " "
          }}</router-link
        >
        <span v-if="project.event"
          >|
          <router-link class="qoollo-link" :to="'/event/' + project.event.id">
            {{ project.event.name }}</router-link
          ></span
        >
      </h1>
    </div>
    <div v-if="project.tags.length > 0 || owner_cond" class="project-tags">
      <Search
        v-if="ready && owner_cond"
        ref="search"
        class="project-tags__search"
        :help="true"
        :active="false"
        :name="'tags'"
        :tags="all_tags"
        :hook="hook"
      />
      <Tag
        v-if="owner_cond"
        class="i-b project-tags__tag project-tags__tag_deletable"
        v-for="(tag, ix) in tags.tags.slice(0, tags.show)"
        :text="tag.name"
        @click="remove_tag(tag, ix)"
      />
      <Tag
        v-else
        class="i-b project-tags__tag"
        v-for="tag in tags.tags.slice(0, tags.show)"
        :text="tag.name"
      />
      <Button
        v-if="tags.tags.length > 4"
        class="i-b"
        @click="show_tags"
        :text="tags.btn_txt"
      />
    </div>
    <div class="project-body i-b">
      <div
        v-if="project.mediaContent.length > 0 || owner_cond"
        class="project-media"
      >
        <div class="project-media__frame">
          <div
            @click="remove_media"
            v-if="owner_cond"
            class="project-media__remove"
          >
            <img
              src="../assets/Project/cross.png"
              alt=""
              class="project-media__remove-img"
            />
          </div>
          <div v-if="owner_cond" class="project-media__add">
            <input
              @change="add_media"
              id="media-loader"
              class="project-media__input-file"
              type="file"
              placeholder="имя"
              name="media"
            />
            <span class="project-media__add-txt">+</span>
          </div>
          <img
            v-for="(media, ix) in media"
            alt=""
            class="project-media__picture"
            :src="media.link"
            :class="{ 'project-media__picture_current': ix == m_show }"
          />
          <div
            v-show="m_show > 0"
            @click="m_show -= 1"
            class="project-media__frame-prev"
          >
            <img
              src="../assets/Project/go_left.png"
              alt=""
              class="project-media__frame-next-img"
            />
          </div>
          <div
            v-show="m_show < project.mediaContent.length - 1"
            @click="m_show += 1"
            class="project-media__frame-next"
          >
            <img
              src="../assets/Project/go_right.png"
              alt=""
              class="project-media__frame-next-img"
            />
          </div>
        </div>
      </div>
      <div class="project-rating">
        <div class="project-rating__vote">
          <div class="project-rating__likes i-b">
            {{ rating.likes }}
            <img
              v-if="project.ratingFlag == 0"
              title="зайдите чтобы голосовать"
              src="../assets/Project/no_upvote.png"
              alt=""
              class="project-rating__likes-btn project-rating__likes-btn_disabled"
            />
            <img
              v-else-if="rating.like == 0"
              @click="like_btn"
              src="../assets/Project/upvote.png"
              alt=""
              class="project-rating__likes-btn"
            />
            <img
              v-else
              @click="like_btn"
              src="../assets/Project/upvoted.png"
              alt=""
              class="project-rating__likes-btn"
            />
          </div>
          <div class="project-rating__dislikes i-b">
            {{ rating.dislikes }}
            <img
              v-if="project.ratingFlag == 0"
              title="зайдите чтобы голосовать"
              src="../assets/Project/no_downvote.png"
              alt=""
              class="project-rating__dislikes-btn project-rating__dislikes-btn_disabled"
            />
            <img
              v-else-if="rating.dislike == 0"
              @click="dislike_btn"
              src="../assets/Project/downvote.png"
              alt=""
              class="project-rating__dislikes-btn"
            />
            <img
              v-else
              @click="dislike_btn"
              src="../assets/Project/downvoted.png"
              alt=""
              class="project-rating__dislikes-btn"
            />
          </div>
        </div>
        <div class="project-rating__date">
          {{ project.project.creationDate.slice(0, 10) }}
        </div>
      </div>
      <div class="project-description">
        <h2>
          Описание
          <img
            @click="redact.md = true"
            v-if="owner_cond && !redact.md"
            src="../assets/Project/redact.png"
            alt=""
            class="project-description__icon"
          />
          <span v-else-if="owner_cond && redact.md">
            <img
              @click="save_md"
              src="../assets/Project/ok.png"
              alt=""
              class="project-description__icon"
            />
            <img
              @click="
                redact.md = false;
                redact.preview = false;
              "
              src="../assets/Project/cross.png"
              alt=""
              class="project-description__icon"
            />
            <span
              class="project-description__mode"
              :class="{ 'project-description__mode_chosen': !redact.preview }"
              @click="redact.preview = false"
              >редактирование</span
            >
            |
            <span
              class="project-description__mode"
              @click="preview"
              :class="{ 'project-description__mode_chosen': redact.preview }"
              >предпросмотр</span
            >
          </span>
        </h2>
        <p v-show="!redact.md" v-html="marked(md)"></p>
        <p id="md_preview" v-show="redact.preview"></p>
        <pre
          id="md_input"
          class="project-description__input"
          v-show="redact.md && !redact.preview"
          contenteditable
          type="text"
          >{{ md }}</pre
        >
      </div>
      <div class="project-users">
        <div class="project-users__list">
          <h2>Участники</h2>
          <div class="project-users__list-avatars i-b">
            <router-link
              v-for="member in project.members.slice(0, 4)"
              :to="'/participantprofile/' + member.nickname"
            >
              <img
                :src="member.photo"
                alt="a"
                class="project-users__list-avatar"
                :title="member.name + ' ' + member.surname"
              />
            </router-link>
          </div>
          <Button
            class="i-b project-users__list-btn"
            v-if="project.members.length > 4"
            @click="open_modal('users')"
            :text="'посмотреть всех'"
          />
        </div>
        <div class="project-users__join">
          <h2 v-if="owner_cond">Заявки</h2>
          <Button
            v-if="project.memberFlag == 3 && !have_sent"
            @click="visible.send_request = true"
            class="project-users__join-btn"
            :text="'Подать заявку'"
          />
          <div
            class="project-users__join-btn"
            v-else-if="have_sent || project.memberFlag == 2"
          >
            <i>Вы подали заявку</i>
          </div>
          <Button
            v-else-if="owner_cond"
            @click="open_modal('watch_requests')"
            class="project-users__join-btn project-users__join-btn_owner"
            :text="requests.length + ' | Смотреть'"
          />
          <div
            class="project-users__join-btn"
            v-else-if="project.memberFlag == 1"
          >
            <i>Вы состоите в проекте</i>
          </div>
        </div>
      </div>
    </div>
    <div class="project-sidepannel i-b">
      <div v-if="ready" class="project-sidepannel__flex-container">
        <Button
          v-if="owner_cond"
          @click="visible.add_news = true"
          class="project-sidepannel__button project-sidepannel__button_mrg"
          :text="'добавить новость'"
        />
        <NewsCard
          v-for="n in news.slice(0, 2)"
          :proj_id="id"
          :title="n.name"
          :text="n.content"
          :id="n.id"
          :date="new Date(n.publishDate)"
        />
        <!--<NewsCard v-for="n in project.news.slice(0, 2)" :title="n.name" :text="n.content" :id="n.id" :date="new Date(n.publishDate)" />-->
        <Button
          @click="open_modal('news')"
          v-if="news.length > 2"
          class="project-sidepannel__button"
          :text="'показать все новости'"
        />
      </div>
    </div>
  </main>
</template>
  
<script>
import marked from "marked";
import NewsCard from "@/components/NewsCard.vue";
import Card from "@/components/Card.vue";
import Tag from "@/components/Tag.vue";
import Button from "@/components/Button.vue";
import Search from "@/components/Search.vue";
import Comment from "@/components/Comment.vue";
import { getDataProject } from "../api/project.js";
import {
  addLike,
  removeLike,
  addDislike,
  removeDislike,
} from "../api/project.js";
import { saveTitle, saveMarkdown } from "../api/project.js";
import { publishNews } from "../api/project.js";
import { addMedia, removeMedia } from "../api/project.js";
import { addTag, removeTag } from "../api/project.js";
import {
  sendRequest,
  getRequests,
  acceptRequest,
  declineRequest,
} from "../api/project.js";

const toBase64 = (file) =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

export default {
  components: {
    NewsCard,
    Tag,
    Button,
    Card,
    Search,
    Comment,
  },
  data: function () {
    return {
      owner_cond: false,
      ready: false,
      visible: {
        users: false,
        news: false,
        add_news: false,
        send_request: false,
        watch_requests: false,
      },
      redact: { title: false, md: false, preview: false },
      title: "",
      md: "",
      tags: { tags: [], show: 4, btn_txt: "показать все" },
      all_tags: [],
      news: [],
      media: [],
      requests: [],
      can_join: false,
      m_show: 0,
      have_sent: false,
      rating: { like: 0, likes: 0, dislike: 0, dislikes: 0 },
      project: {},
    };
  },
  methods: {
    marked,
    async getData() {
      this.project = JSON.parse(
        '{\
            "project": {\
                "name": "",\
                "markdown": "",\
                "creationDate": "2021-07-01T00:00:00"\
            },\
            "owner": {\
                "photo": "",\
                "nickname": "",\
                "name": "",\
                "surname": ""\
            },\
            "event": {\
                "id": 1,\
                "name": ""\
            },\
            "mediaContent": [],\
            "like": 0,\
            "dislike": 0,\
            "ratingFlag": 0,\
            "news": [],\
            "tags": [],\
            "members": [],\
            "requests": []\
        }'
      );
      const res = await getDataProject(this.id, this.token);
      if (res.status == 500) window.location.href = "/404";
      else {
        this.project = await res.json();
        if (this.user)
          this.owner_cond = this.project.owner.nickname == this.user.user.url;

        this.news = this.project.news.reverse();
        this.media = this.project.mediaContent;

        this.rating.likes = this.project.like;
        this.rating.dislikes = this.project.dislike;
        if (this.project.ratingFlag == 1) this.rating.dislike = 1;
        if (this.project.ratingFlag == 2) this.rating.like = 1;
        console.log(this.project, this.user, this.owner_cond);
      }
    },
    show_tags() {
      if (this.tags.show == this.tags.tags.length) {
        this.tags.btn_txt = "показать все";
        this.tags.show = 4;
      } else {
        this.tags.show = this.tags.tags.length;
        this.tags.btn_txt = "скрыть";
      }
    },
    media_prev() {
      this.m_show -= 1;
    },
    async like_btn() {
      if (this.rating.like == 0) {
        addLike(this.id, this.token, this.rating.dislike);

        if (this.rating.dislike == 1) {
          this.rating.dislikes -= 1;
          this.rating.dislike = 0;
        }

        this.rating.like = 1;
        this.rating.likes += 1;
      } else {
        removeLike(this.id, this.token);

        this.rating.like = 0;
        this.rating.likes -= 1;
      }
    },
    async dislike_btn() {
      if (this.rating.dislike == 0) {
        addDislike(this.id, this.token, this.rating.like);

        if (this.rating.like == 1) {
          this.rating.likes -= 1;
          this.rating.like = 0;
        }

        this.rating.dislike = 1;
        this.rating.dislikes += 1;
      } else {
        removeDislike(this.id, this.token);

        this.rating.dislike = 0;
        this.rating.dislikes -= 1;
      }
    },
    async save_title() {
      this.title = document.getElementById("title_input").innerHTML;
      this.redact.title = false;

      saveTitle(this.id, this.token, this.title);
    },
    preview() {
      this.redact.preview = true;
      let html = document.getElementById("md_input").innerHTML;
      let temp = html.replace("<br>", "\n");
      while (html != temp) {
        html = temp;
        temp = temp.replace("<br>", "\n");
      }
      document.getElementById("md_preview").innerHTML = marked(html);
    },
    async save_md() {
      this.md = document.getElementById("md_input").innerHTML;
      let temp = this.md.replace("<br>", "\n");
      while (this.md != temp) {
        this.md = temp;
        temp = temp.replace("<br>", "\n");
      }
      this.redact.preview = false;
      this.redact.md = false;

      saveMarkdown(this.id, this.token, this.md);
    },
    async publish() {
      let t_i = document.getElementById("news-title");
      let c_i = document.getElementById("news-contents");
      let title = t_i.value;
      let contents = c_i.value;
      publishNews(this.id, this.token, title, contents).then(async (res) => {
        if (res.ok) {
          let id = await res.text();
          t_i.value = "";
          c_i.value = "";
          this.visible.add_news = false;
          this.news.unshift({
            id: id,
            name: title,
            content: contents,
            publishDate: Date.now(),
          });
        }
      });
    },
    async add_media() {
      let f = document.getElementById("media-loader").files[0];
      const result = await toBase64(f).catch((e) => Error(e));

      addMedia(this.id, this.token, result, 0);

      this.media.push({ link: result, type: 0 });
      this.m_show = this.media.length - 1;
    },
    async remove_media() {
      if (this.media.length > 0) {
        let f = this.media[this.m_show].link;

        removeMedia(this.id, this.token, f, 0);

        this.media.splice(this.m_show, 1);
        if (this.m_show == this.media.length) this.m_show -= 1;
      }
    },
    async hook(tag) {
      addTag(this.id, this.token, tag.id, tag.name, tag.color).then((r) => {
        if (r.ok) {
          this.tags.tags.push(tag);
          if (this.tags.show > 4) this.tags.show += 1;
        }
        this.$refs.search.clear();
      });
    },
    async remove_tag(tag, index) {
      removeTag(this.id, this.token, tag.id, tag.name, tag.color);

      this.tags.tags.splice(index, 1);
    },
    async send_request() {
      let c_i = document.getElementById("request-contents");
      let contents = c_i.value;
      sendRequest(this.id, this.token, contents).then(async (res) => {
        if (res.ok) {
          c_i.value = "";
          this.have_sent = true;
          this.visible.send_request = false;
        }
      });
    },
    async accept_request(devId, ix) {
      acceptRequest(this.id, this.token, devId).then(async (res) => {
        if (res.ok) {
          //this.have_sent = true;
          //this.visible.send_request = false;
          this.requests.splice(ix, 1);
        }
      });
    },
    async decline_request(devId, ix) {
      declineRequest(this.id, this.token, devId).then(async (res) => {
        if (res.ok) {
          //this.have_sent = true;
          //this.visible.send_request = false;
          this.requests.splice(ix, 1);
        }
      });
    },
    open_modal(vr) {
      this.visible[vr] = true;
      document.getElementsByTagName("body")[0].style["overflow-y"] = "hidden";
    },
    close_modal(vr) {
      this.visible[vr] = false;
      document.getElementsByTagName("body")[0].style["overflow-y"] = "auto";
    },
  },
  async created() {
    this.id = this.$route.params.id;
    this.user = this.$root.get_user();
    if (this.user) this.token = this.user.access_token;
    else this.token = "";

    await this.getData();
    this.title = this.project.project.name;
    this.md = this.project.project.markdown;
    this.tags.tags = this.project.tags;
    this.can_join = this.project.memberFlag == 3;
    if (this.owner_cond) {
      this.all_tags = await this.$root.get_tags();
      //let req = await getRequests(this.id, this.token);
      this.requests = this.project.requests; //await req.json();
    }
    this.ready = true;
  },
};
</script>

<style lang="scss">
@import "../assets/Project/style.scss";
</style>
