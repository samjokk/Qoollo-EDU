<template>
  <div class="art-wave art-wave_users"></div>
  <main>
    <div class="users-container">
      <div class="users-searchbar">
        <div class="users-searchbar__query i-b">
          найди участников и пригласи их в свою команду
          <Search
            :onclick="start_search"
            :help="false"
            :active="true"
            :name="'query'"
            :tags="[]"
          />
        </div>
        <div class="users-searchbar__filters i-b">
          <div class="users-searchbar__tags i-b">
            теги
            <Search
              v-if="ready"
              :help="true"
              :active="false"
              :name="'query_tags'"
              :tags="users.tags"
            />
          </div>
          <div class="users-searchbar__rating i-b">
            рейтинг
            <div class="users-searchbar__rating-radio">
              <label class="radio">
                <input
                  name="rating"
                  type="radio"
                  value="1"
                  class="radio__btn"
                  checked="1"
                />
                <span class="radio__circle radio__face"></span>
                <span class="radio__text">по убыванию</span>
              </label>
              <label class="radio">
                <input
                  name="rating"
                  type="radio"
                  value="2"
                  class="radio__btn"
                />
                <span class="radio__circle radio__face"></span>
                <span class="radio__text">по возрастанию</span>
              </label>
            </div>
          </div>
        </div>
      </div>
      <div>
        <div class="users-result">
          <UserCard v-for="d in users_show" :dto="d" />
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import Button from "@/components/Button.vue";
import UserCard from "@/components/UserCard.vue";
import Search from "@/components/Search.vue";
let backend = "http://localhost:5000";

async function getDataProject() {
  const res = await fetch(backend + "/api/search/developers", {
    method: "GET",
    // headers: {
    //     Authorization: token
  });
  return await res;
}

export default {
  components: {
    Button,
    UserCard,
    Search,
  },
  data: function () {
    return {
      ready: false,
      users_show: [],
    };
  },
  methods: {
    async getData() {
      this.users = { developers: [], tags: [] };
      const res = await getDataProject();
      this.users = await res.json();
      this.users_show = this.users.developers;
      console.log(this.users);
      this.ready = true;
    },
    start_search() {
      let tags = document.getElementsByName("query_tags")[0].value.trim();
      if (tags.length > 0) tags = tags.split(" ");
      else tags = [];

      let qs = document.getElementsByName("query")[0].value.trim();
      if (qs.length > 0) qs = qs.split(" ");
      else qs = [];

      this.users_show = [];
      this.users.developers.forEach((u) => {
        let flag = false;
        if (!tags.length) flag = true;
        for (let i = 0; !flag && i < tags.length; i++)
          for (let j = 0; !flag && j < u.tags.length; j++)
            if (u.tags[j].name == tags[i]) flag = true;

        let flag1 = false;
        for (let i = 0; !flag1 && i < qs.length; i++)
          if (
            u.name.includes(qs[i]) ||
            u.surname.includes(qs[i]) ||
            u.nickname.includes(qs[i])
          )
            flag1 = true;
        if (!qs.length) flag1 = true;

        if (flag && flag1) this.users_show.push(u);
      });

      if (document.querySelector('input[name="rating"]:checked').value == 1)
        this.users_show.sort(function (a, b) {
          if (a.rating > b.rating) return -1;
          else return 1;
        });
      else
        this.users_show.sort(function (a, b) {
          if (a.rating < b.rating) return -1;
          else return 1;
        });
    },
  },
  created() {
    this.getData();
    this.$root.$refs.header.printMenu(3);
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
  &_users {
    background-image: url("../assets/Users/wave.png");
    background-position-x: -520px;
    background-position-y: 162px;
  }
}

.users-container {
  display: flex;
  flex-direction: column;
  width: 1000px;
}

.radio {
  display: block;
  height: 22px;
  &:hover {
    cursor: pointer;
  }
  &__btn {
    display: none;
    &:checked ~ .radio__circle::before {
      background: $main-color;
      background-clip: text;
      -webkit-text-fill-color: transparent;
      content: "⦿";
    }
  }
  & > &__circle::before {
    content: "⭘";
  }
  &__face {
    margin-right: 10px;
  }
  &__text {
    vertical-align: top;
  }
}

.users-searchbar {
  margin-bottom: 25px;
  &__query,
  &__filters {
    width: calc(47% + 20px);
    text-align: center;
    vertical-align: top;
  }
  &__query {
    margin-right: calc(6% - 40px);
  }
  &__tags {
    width: 50%;
    margin-right: 30px;
  }
  &__rating,
  &__tags {
    vertical-align: top;
  }
  &__rating {
    &-radio {
      text-align: left;
    }
  }
}
.users-result {
  display: flex;
  flex-wrap: wrap;
  gap: calc(6% - 40px);
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
