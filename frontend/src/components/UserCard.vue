<template>
  <div class="user-card">
    <router-link :to="'/participantprofile/' + dto.nickname">
      <Card />
      <div class="user-card__personal-info">
        <img
          :src="dto.photo"
          alt=""
          class="user-card__avatar"
        />
        <div class="user-card__text-info">
          <p class="user-card__info-left">{{ dto.name + " " + dto.surname }}</p>
          <p class="user-card__info-right">{{ dto.nickname }}</p>
          <br />
          <p class="user-card__info-left">{{ format_age(dto.age) }}</p>
          <p class="user-card__info-right">Рейтинг: {{ dto.rating }}</p>
        </div>
      </div>
      <div class="user-card__tags">
        <Tag v-for="tag in dto.tags" :text="tag.name" />
      </div>
    </router-link>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";
import Tag from "@/components/Tag.vue";

export default {
  components: {
    Card,
    Tag,
  },
  props: {
    dto: Object,
  },
  methods: {
    format_age(age) {
      if (age % 10 == 1) return age + " год";
      else if (age % 10 > 4 || age % 10 == 0) return age + " лет";
      else return age + " года";
    },
  },
};
</script>


<style lang="scss">
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$card-border: linear-gradient(
  90deg,
  rgba(73, 73, 73, 1) 0%,
  rgba(0, 0, 0, 0) 100%
);
$card-bg: rgba(39, 39, 47, 0.4);
$txt-color: #f8f8ff;

.user-card {
  flex-basis: 47%;
  margin-bottom: 20px;
  padding: 10px;
  position: relative;
  & .tag {
    color: $txt-color;
  }
  &__avatar {
    border-radius: 50%;
    width: 50px;
    height: 50px;
    margin-right: 20px;
  }
  &__text-info {
    width: calc(100% - 70px);
  }
  &__text-info {
    display: inline-block;
    vertical-align: top;
  }
  &__info-right {
    text-align: right;
  }
  &__info-right,
  &__info-left {
    vertical-align: top;
    display: inline-block;
    margin: 0;
    width: 50%;
  }

  &__tags {
    display: flex;
    gap: 10px;
    flex-wrap: wrap;
  }

  &:hover {
    cursor: pointer;
    .bg-card {
      background-color: rgba(39, 39, 47, 0.8);
    }
  }
}

.qoollo-link {
  background: $main-color;
  background-clip: text;
  -webkit-text-fill-color: transparent;
  text-decoration: none;
}

.news-card {
  padding: 10px 20px;
  width: fit-content;
  max-width: 250px;
  position: relative;
  color: $txt-color;
  margin-bottom: 15px;
}
</style>
