<template>
  <div class="comment">
    <div class="i-b">
      <router-link :to="'/praticipantprofile/' + dto.user.nickname">
        <img :src="dto.user.photo" alt="A" class="comment__avatar">
      </router-link>
    </div>
    <div class="comment__body i-b">
      <router-link :to="'/praticipantprofile/' + dto.user.nickname">
        <span class="comment__name">{{dto.user.name + ' ' + dto.user.surname}}</span>
      </router-link>
      <p>{{dto.comment}}</p>
    </div>
  </div>
</template>

<script>

export default {
  components: {},
  props: {
    dto: Object
  },
};
</script>


<style lang="scss">
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);
$txt-color: #f8f8ff;

.qoollo-header {
  font-size: 30;
  margin: 0 0;
}

.qoollo-link {
    background: $main-color;
    background-clip: text;
    -webkit-text-fill-color: transparent;
    text-decoration: none;
}

.comment {
  margin-bottom: 30px;
  &__avatar {
    border-radius: 50%;
    width: 50px;
    height: 50px;
  }

  &__body {
    vertical-align: top;
    margin-left: 10px;
    width: calc(100% - 60px);
  }

  &__name {
    font-weight: bold;
  }
}
</style>
