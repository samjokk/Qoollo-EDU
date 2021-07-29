<template>
  <div class="top-projects-events">
    <div class="title">
      <p v-if="projectEventFlag">Топ проектов</p>
      <p v-else>Топ событий</p>
    </div>
    <ul id="ul-top-projects-events">
      <li v-for="projectEvent in cards" :key="projectEvent">
        <div class="top-project-event">
          <!-- Пока временно берём фотку -->
          <img
            src="../../assets/CardProjectEvent/AvatarProject.png"
            alt="Avatar"
          />
          <p>{{ projectEvent.name }}</p>
          <router-link v-if="projectEventFlag" to="/project"
            ><img
              src="../../assets/ProjectsEventsPage/LinkItem.svg"
              alt="Link"
              class="link-img"
          /></router-link>
          <router-link v-else to="/event"
            ><img
              src="../../assets/ProjectsEventsPage/LinkItem.svg"
              alt="Link"
              class="link-img"
          /></router-link>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    cards: Object,
    projectEventFlag: Boolean,
  },
  async mounted() {},
};
</script>

<style lang="scss">
$color-card: rgba(33, 33, 33, 0.5);

%display-between {
  display: flex;
  justify-content: space-between;
}

.top-projects-events {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  .title {
    width: 100%;
    height: 45px;
    margin-top: 30px;
    font-size: 24px;
    text-align: center;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  #ul-top-projects-events {
    width: 240px;
    height: 450px;
    list-style-type: none;
    margin: 10px 0px 0px 0px;
    padding: 0;
    li {
      width: 240px;
      height: 36px;
      margin-top: 10px;
      .top-project-event {
        @extend %display-between;
        align-items: center;
        .link-img {
          width: 19px;
          height: 20px;
        }
        img {
          width: 36px;
          height: 36px;
        }
        p {
          font-size: 14px;
        }
      }
    }
  }
  p {
    margin: 0;
    max-width: 170px;
    text-align: center;
  }
}
</style>