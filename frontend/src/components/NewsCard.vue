<template>
    <div class="news-card">
      <Card />
      <h2 class="news-card__title">{{title}}</h2>
      <span class="news-card__date">{{date.toISOString().slice(0,10).replace(/-/g,".")}}</span>
      <p>{{text.slice(0, 200)}}</p>
      <router-link class="qoollo-link" :to ="'/project/' + proj_id + '/read/' + id">читать дальше</router-link>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";

export default {
  components: {
    Card,
  },
  props: {
    title: String,
    text: String,
    id: Number,
    proj_id: String,
    date: Date
  }
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

.news-card {
  padding: 10px 20px;
  //width: fit-content;
  max-width: 250px;
  position: relative;
  color: $txt-color;
  margin-bottom: 15px;
  word-break: break-word;
  &__date {
    font-style: italic;
    color: #c3c3c4 !important;
  }
}
</style>
