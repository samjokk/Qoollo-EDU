<template>
    <div class="tag">
      <Card />
      {{text}}
  </div>
</template>

<script>
import Card from "@/components/Card.vue";

export default {
  components: {
    Card,
  },
  props: {
    text: String,
  }
};
</script>


<style lang="scss">
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);

.tag {
  padding: 3px 15px;
  position: relative;
  text-align: center;
  width: fit-content;
}
</style>
