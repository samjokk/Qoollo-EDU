<template>
  <div class="art-wave art-wave_event"></div>

  <div v-show="projects_visible" class="page-shadow"></div>
  <div v-show="projects_visible" class="modal">
    <Card />
    <div @click="projects_visible = false" class="modal__exit">
      <img src="../assets/Project/cross.png" alt="" class="modal__exit-img" />
    </div>
    <h2>Проекты</h2>
    <div class="modal__body modal__body_scroll">
      <ProjectCard
        class="modal__project-card"
        v-for="project in event.eventProjects"
        :title="project.name"
        :partitioners="project.membersCount"
        :date="new Date(project.creationDate)"
        :id="project.id"
      />
    </div>
  </div>
  <main>
    <div class="event-header">
      <h1>
        <span v-if="!redact.title">{{ title }}</span>
        <span
          v-else
          type="text"
          id="title_input"
          class="qoollo-header event-header__title-input"
          contenteditable
          >{{ title }}</span
        >
        <img
          @click="redact.title = true"
          v-if="owner_cond && !redact.title"
          src="../assets/Project/redact.png"
          alt=""
          class="event-header__icon"
        />
        <span v-else-if="owner_cond && redact.title">
          <img
            @click="save_title"
            src="../assets/Project/ok.png"
            alt=""
            class="event-header__icon"
          />
          <img
            @click="redact.title = false"
            src="../assets/Project/cross.png"
            alt=""
            class="event-header__icon"
          />
        </span>
        |
        <router-link
          class="qoollo-link"
          :to="'/organizerprofile/' + event.organizer.id"
          ><img
            :src="event.organizer.photo"
            alt=""
            class="event-header__avatar"
          />
          {{ event.organizer.name }}</router-link
        >
      </h1>
    </div>
    <div v-if="event.tags.length > 0 || owner_cond" class="event-tags">
      <Search
        v-if="ready && owner_cond"
        ref="search"
        class="event-tags__search"
        :help="true"
        :active="false"
        :name="'tags'"
        :tags="all_tags"
        :hook="hook"
        :placeholder="'поиск тегов'"
      />
      <Tag
        v-if="owner_cond"
        class="i-b event-tags__tag event-tags__tag_deletable"
        v-for="(tag, ix) in tags.tags.slice(0, tags.show)"
        :text="tag.name"
        @click="remove_tag(tag, ix)"
      />
      <Tag
        v-else
        class="i-b event-tags__tag"
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
    <div class="event-body i-b">
      <ProgBar :state="state" />
      <div class="event-rating">
        <div class="event-rating__vote">
          <div class="event-rating__likes i-b">
            {{ rating.likes }}
            <img
              v-if="event.ratingFlag == 0"
              title="зайдите чтобы голосовать"
              src="../assets/Project/no_upvote.png"
              alt=""
              class="event-rating__likes-btn event-rating__likes-btn_disabled"
            />
            <img
              v-else-if="rating.like == 0"
              @click="like_btn"
              src="../assets/Project/upvote.png"
              alt=""
              class="event-rating__likes-btn"
            />
            <img
              v-else
              @click="like_btn"
              src="../assets/Project/upvoted.png"
              alt=""
              class="event-rating__likes-btn"
            />
          </div>
          <div class="event-rating__dislikes i-b">
            {{ rating.dislikes }}
            <img
              v-if="event.ratingFlag == 0"
              title="зайдите чтобы голосовать"
              src="../assets/Project/no_downvote.png"
              alt=""
              class="event-rating__dislikes-btn event-rating__dislikes-btn_disabled"
            />
            <img
              v-else-if="rating.dislike == 0"
              @click="dislike_btn"
              src="../assets/Project/downvote.png"
              alt=""
              class="event-rating__dislikes-btn"
            />
            <img
              v-else
              @click="dislike_btn"
              src="../assets/Project/downvoted.png"
              alt=""
              class="event-rating__dislikes-btn"
            />
          </div>
        </div>
        <div class="event-rating__date">
          {{
            event.event.startDate.slice(0, 10) +
            "  / " +
            event.event.endDate.slice(0, 10)
          }}
        </div>
      </div>
      <div class="event-description">
        <h2>
          О событии
          <img
            @click="redact.md = true"
            v-if="owner_cond && !redact.md"
            src="../assets/Project/redact.png"
            alt=""
            class="event-description__icon"
          />
          <span v-else-if="owner_cond && redact.md">
            <img
              @click="save_md"
              src="../assets/Project/ok.png"
              alt=""
              class="event-description__icon"
            />
            <img
              @click="
                redact.md = false;
                redact.preview = false;
              "
              src="../assets/Project/cross.png"
              alt=""
              class="event-description__icon"
            />
            <span
              class="event-description__mode"
              :class="{ 'event-description__mode_chosen': !redact.preview }"
              @click="redact.preview = false"
              >редактирование</span
            >
            |
            <span
              class="event-description__mode"
              @click="preview"
              :class="{ 'event-description__mode_chosen': redact.preview }"
              >предпросмотр</span
            >
          </span>
        </h2>
        <p v-show="!redact.md" v-html="marked(md)"></p>
        <p id="md_preview" v-show="redact.preview"></p>
        <pre
          id="md_input"
          class="event-description__input"
          v-show="redact.md && !redact.preview"
          contenteditable
          type="text"
          >{{ md }}</pre
        >
      </div>
    </div>
    <div class="event-sidebar i-b">
      <router-link
        v-if="can_participate && state < 1"
        :eventId="1"
        :to="{
          name: 'CreateProject',
          params: { eventId: id, eventName: event.event.name },
        }"
      >
        <Button :text="'Принять участие'"
      /></router-link>
      <div class="event-projects">
        <h2>Проекты</h2>
        <ProjectCard
          v-for="project in event.eventProjects.slice(0, 2)"
          :title="project.name"
          :partitioners="project.membersCount"
          :date="new Date(project.creationDate)"
          :id="project.id"
        />
        <Button
          @click="projects_visible = true"
          v-if="event.eventProjects.length > 2"
          class="event-projects__load-btn"
          :text="'посмотреть все'"
        />
        <i v-if="event.eventProjects.length == 0"
          >В рамках события ещё не создано проектов</i
        >
      </div>
    </div>
  </main>
</template>

<script>
import marked from "marked";
import Button from "@/components/Button.vue";
import Card from "@/components/Card.vue";
import ProjectCard from "@/components/ProjectCard.vue";
import Tag from "@/components/Tag.vue";
import ProgBar from "@/components/ProgBar.vue";
import Search from "@/components/Search.vue";
let backend = "http://localhost:5000";

async function getDataProject(id, token) {
  const res = await fetch(backend + "/api/event/" + id, {
    method: "GET",
    headers: {
      Authorization: "Bearer " + token,
    },
  });
  return await res;
}

export default {
  components: {
    Button,
    Card,
    Tag,
    ProgBar,
    ProjectCard,
    Search,
  },
  data: function () {
    return {
      projects_visible: false,
      tags: { tags: [], show: 4, btn_txt: "показать все" },
      news: { show: 2, btn_visible: true },
      rating: { like: 0, likes: 0, dislike: 0, dislikes: 0 },
      owner_cond: false,
      ready: false,
      redact: { title: false, md: false, preview: false },
      can_participate: false,
      title: "",
      md: "",
      all_tags: [],
      event: {},
      state: 0,
    };
  },
  methods: {
    marked,
    async getData() {
      this.event = {
        organizer: {
          id: 2,
          photo: "",
          name: "",
        },
        event: {
          name: "",
          description: "",
          startDate: "",
          endDate: "",
        },
        projectNum: 0,
        tags: [],
        like: 0,
        dislike: 0,
        eventProjects: [],
        projectPlaces: [],
        ratingFlag: 0,
      };
      const res = await getDataProject(this.$route.params.id, this.token);
      if (res.status == 500) window.location.href = "/404";
      else {
        this.event = await res.json();
        console.log(this.event);
        if (this.user)
          this.owner_cond = this.event.organizer.id == this.user.user.url;

        this.rating.likes = this.event.like;
        this.rating.dislikes = this.event.dislike;
        if (this.event.ratingFlag == 1) this.rating.dislike = 1;
        if (this.event.ratingFlag == 2) this.rating.like = 1;
      }
    },
    all_tags() {
      if (this.tags.show == this.event.tags.length) {
        this.tags.btn_txt = "показать все";
        this.tags.show = 4;
      } else {
        this.tags.show = this.event.tags.length;
        this.tags.btn_txt = "скрыть";
      }
    },
    async like_btn() {
      if (this.rating.like == 0) {
        let method = "POST";
        if (this.rating.dislike == 1) {
          method = "PUT";
          this.rating.dislikes -= 1;
          this.rating.dislike = 0;
        }
        await fetch(backend + "/api/event/" + this.id + "/rating", {
          method: method,
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + this.token,
          },
          body: JSON.stringify({ rating: 1 }),
        });
        this.rating.like = 1;
        this.rating.likes += 1;
      } else {
        await fetch(backend + "/api/event/" + this.id + "/rating", {
          method: "DELETE",
          headers: {
            Accept: "application/json;charset=utf-8",
            "Content-Type": "application/json;charset=utf-8",
            Authorization: "Bearer " + this.token,
          },
        });
        this.rating.like = 0;
        this.rating.likes -= 1;
      }
    },
    async dislike_btn() {
      if (this.rating.dislike == 0) {
        let method = "POST";
        if (this.rating.like == 1) {
          method = "PUT";
          this.rating.likes -= 1;
          this.rating.like = 0;
        }
        await fetch(backend + "/api/event/" + this.id + "/rating", {
          method: method,
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + this.token,
          },
          body: JSON.stringify({ rating: 0 }),
        });
        this.rating.dislike = 1;
        this.rating.dislikes += 1;
      } else {
        await fetch(backend + "/api/event/" + this.id + "/rating", {
          method: "DELETE",
          headers: {
            Accept: "application/json;charset=utf-8",
            "Content-Type": "application/json;charset=utf-8",
            Authorization: "Bearer " + this.token,
          },
        });
        this.rating.dislike = 0;
        this.rating.dislikes -= 1;
      }
    },
    async hook(tag) {
      await fetch(backend + "/api/event/" + this.id + "/tag", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + this.token,
        },
        body: JSON.stringify({
          id: tag.id,
          name: tag.name,
          color: tag.color,
        }),
      }).then((r) => {
        if (r.ok) {
          this.tags.tags.push(tag);
          if (this.tags.show > 4) this.tags.show += 1;
        }
        this.$refs.search.clear();
      });
    },
    async remove_tag(tag, index) {
      await fetch(backend + "/api/event/" + this.id + "/tag", {
        method: "DELETE",
        headers: {
          Accept: "application/json;charset=utf-8",
          "Content-Type": "application/json;charset=utf-8",
          Authorization: "Bearer " + this.token,
        },
        body: JSON.stringify({
          id: tag.id,
          name: tag.name,
          color: tag.color,
        }),
      });
      this.tags.tags.splice(index, 1);
    },
    async save_title() {
      this.title = document.getElementById("title_input").innerHTML;
      this.redact.title = false;

      await fetch(backend + "/api/event/" + this.id + "/name", {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + this.token,
        },
        body: JSON.stringify({ newName: this.title }),
      });
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

      await fetch(backend + "/api/event/" + this.id + "/markdown", {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + this.token,
        },
        body: JSON.stringify({ newMarkdown: this.md }),
      });
    },
  },
  async created() {
    this.id = this.$route.params.id;
    this.user = this.$root.get_user();
    console.log(this.user);
    if (this.user) {
      this.token = this.user.access_token;
      if (this.user.user.roleUrl == "profile") this.can_participate = true;
    } else this.token = "";

    await this.getData();
    this.title = this.event.event.name;
    this.md = this.event.event.description;
    this.tags.tags = this.event.tags;

    let date = new Date(Date.now());
    if (new Date(this.event.event.startDate) > date) this.state = 0;
    else if (new Date(this.event.event.endDate) > date) this.state = 1;
    else this.state = 2;

    if (this.owner_cond) this.all_tags = await this.$root.get_tags();
    this.ready = true;
  },
};
</script>

<style lang="scss">
@import "../assets/Event/style.scss";
</style>
