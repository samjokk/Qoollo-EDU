<template>
  <div class="art-wave art-wave_read-1"></div>
  <div class="art-wave art-wave_read-2"></div>

  <main>
    <div class="read-left-bar i-b">
      <NewsCard
        v-if="ready && page.prevNews.name"
        :proj_id="$route.params.proj_id"
        :title="page.prevNews.name"
        :text="page.prevNews.content"
        :id="page.prevNews.id"
        :date="new Date(page.prevNews.publishDate)"
      />
    </div>
    <div class="read-body i-b">
      <h1>
        {{ page.curNews.name }} |
        <router-link
          v-if="ready"
          class="qoollo-link"
          :to="'/project/' + page.id"
          >{{ page.name }}</router-link
        >
      </h1>
      <span class="read-body__date">{{
        new Date(page.curNews.publishDate)
          .toISOString()
          .slice(0, 10)
          .replace(/-/g, ".")
      }}</span>
      <div class="read-contents">{{ page.curNews.content }}</div>
      <h2>Комментарии</h2>
      <div class="read-comments">
        <div v-if="can_comment" class="comment">
          <div class="i-b">
            <router-link :to="'/praticipantprofile/' + user.user.url">
              <img :src="user.user.photo" alt="A" class="comment__avatar" />
            </router-link>
          </div>
          <div class="comment__body i-b">
            <router-link :to="'/praticipantprofile/' + user.user.url">
              <span class="comment__name">{{
                user.user.name + " " + user.user.surname
              }}</span> </router-link
            ><br />
            <textarea
              id="comment_txt"
              class="read-comments__textarea"
              name=""
              cols="30"
              rows="3"
            ></textarea>
          </div>
          <div class="read-comments__submit">
            <Button @click="submit_comment" :text="'Отправить'" />
          </div>
        </div>
        <Comment v-for="c in comments" :dto="c" />
        <i v-if="!comments.length">Здесь ещё нет комментариев</i>
      </div>
    </div>
    <div class="read-right-bar i-b">
      <NewsCard
        v-if="ready && page.nextNews.name"
        :proj_id="$route.params.proj_id"
        :title="page.nextNews.name"
        :text="page.nextNews.content"
        :id="page.nextNews.id"
        :date="new Date(page.nextNews.publishDate)"
      />
    </div>
  </main>
</template>

<script>
import NewsCard from "@/components/NewsCard.vue";
import Comment from "@/components/Comment.vue";
import Button from "@/components/Button.vue";
let backend = "http://localhost:5000";

async function getDataNews(id, proj_id) {
  const res = await fetch(backend + "/api/project/" + proj_id + "/news/" + id, {
    method: "GET",
    // headers: {
    //     Authorization: token
  });
  return await res;
}

export default {
  components: {
    NewsCard,
    Comment,
    Button,
  },
  data: function () {
    return {
      ready: false,
      comments: [],
      can_comment: false,
    };
  },
  methods: {
    async getData(id, proj_id) {
      this.page = {
        id: 0,
        name: "s",
        prevNews: {
          id: 0,
          name: null,
          content: null,
          publishDate: "0001-01-01T00:00:00",
        },
        curNews: {
          id: 1,
          name: "",
          content: "",
          publishDate: "2021-07-01T00:00:00",
        },
        nextNews: {
          id: 0,
          name: null,
          content: null,
          publishDate: "0001-01-01T00:00:00",
        },
        comments: [],
      };
      const res = await getDataNews(id, proj_id);
      if (res.status == 500) window.location.href = "/404";
      else {
        this.page = await res.json();
        console.log(this.page);
        this.comments = this.page.comments.reverse();
      }
    },
    async submit_comment() {
      let text = document.getElementById("comment_txt").value;

      await fetch(
        backend + "/api/project/news/" + this.$route.params.id + "/comment",
        {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + this.token,
          },
          body: JSON.stringify({ comment: text }),
        }
      );

      this.comments.unshift({
        user: {
          name: this.user.user.name,
          surname: this.user.user.surname,
          nickname: this.user.user.nickname,
          photo: this.user.user.photo,
        },
        comment: text,
      });
    },
  },
  async created() {
    this.id = this.$route.params.id;
    this.proj_id = this.$route.params.proj_id;

    this.user = this.$root.get_user();
    if (this.user) {
      this.token = this.user.access_token;
      if (this.user.user.roleUrl == "profile") this.can_comment = true;
    } else this.token = "";

    await this.getData(this.id, this.proj_id);

    this.ready = true;
  },
  watch: {
    $route(to, from) {
      this.getData(to.params.id, to.params.proj_id);
    },
  },
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
  &_read-1 {
    background-image: url("../assets/Read/wave1.png");
    background-position-x: -520px;
    background-position-y: 162px;
  }
  &_read-2 {
    background-image: url("../assets/Read/wave2.png");
    background-position-x: 1220px;
    background-position-y: 412px;
  }
}

.read {
  &-body {
    width: 780px;
    vertical-align: top;
    margin: 0 50px;
    &__date {
      font-style: italic;
      color: #c3c3c4 !important;
    }
  }

  &-right-bar,
  &-left-bar {
    width: 250px;
    vertical-align: top;
  }

  &-contents {
    margin-bottom: 30px;
    word-break: break-word;
  }

  &-comments {
    padding: 20px 0;
    &__textarea {
      width: calc(100% - 10px);
      padding: 0;
      background: $bg-color;
      color: $txt-color;
      padding: 5px;
      border: 1px solid #f8f8ff;
      border-radius: 5px;
      overflow: auto;
      outline: none;

      -webkit-box-shadow: none;
      -moz-box-shadow: none;
      box-shadow: none;

      font-family: inherit;
      font-size: inherit;
    }

    &__submit {
      margin-top: 5px;
      width: 100%;
      display: flex;
      justify-content: flex-end;
    }
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
