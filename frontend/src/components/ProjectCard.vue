<template>
  <div class="project-card">
    <Card />
    <router-link class="project-card__link" :to="'/project/'+ id">
    <img src="../assets/Event/proj_logo.png" alt="" class="project-card__logo">
    <p class="project-card__title">{{title}}</p>
    <div class="project-card__users">
      <img src="../assets/Event/part_count.png" alt="" class="project-card__users-icon"> <br>
      {{partitioners}}
    </div>
    <div class="project-card__date">
      <img src="../assets/Event/proj_date.png" alt="" class="project-card__date-icon"> <br>
      {{date.toISOString().slice(0,10).replace(/-/g,".")}}
    </div>
    </router-link>
  </div>
</template>

<script>
import Card from "@/components/Card.vue";

export default {
  components: {Card},
  props: {
    title: String,
    partitioners: Number,
    date: Date,
    id: Number
  }
};
</script>

<style lang="scss">
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);
$txt-color: #f8f8ff;

.project-card {
  margin: 10px 0;
  padding: 10px;
  position: relative;
  &__link {
    color: $txt-color;
  }
  &__logo {
    vertical-align: middle;
    margin-right: 10px;
    height: 50px;
    width: 50px;
    border-radius: 50%;
  }
  &__title {
    display: inline-block;
    //max-width: 300px;
    vertical-align: middle;
    margin: 0;
    text-align: center;
    width: calc(100% - 210px);
  }
  &__users {
    display: inline-block;
    vertical-align: middle;
    margin-right: 20px;
    width: 40px;
    text-align: center;
  }
  &__date {
    display: inline-block;
    vertical-align: middle;
    text-align: center;
  }

  &:hover {
    cursor: pointer;
    .bg-card {
      background-color: rgba(39, 39, 47, 0.8);
    }
  }
}
</style>
