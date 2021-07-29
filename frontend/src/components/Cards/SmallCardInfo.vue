<template>
  <div class="small-card-info">
    <p class="text-info-header">{{ title }}</p>
    <p class="text-info">{{ text }}</p>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    title: String,
    text: String,
  },
};
</script>

<style lang="scss">
$card-bg: rgba(39, 39, 47, 0.4);
.small-card-info {
  width: 400px;
  height: 280px;
  border-radius: 10px;
  background-color: $card-bg;
  backdrop-filter: blur(20px);
  background: url("../../assets/Landing/SmallCardBackground.png");
  .text-info-header {
    padding-top: 20px;
    margin: 0px 20px 0px 20px;
    font-family: Raleway;
    font-size: 24px;
  }
  .text-info {
    margin: 30px 20px 20px 20px;
  }
}
</style>
