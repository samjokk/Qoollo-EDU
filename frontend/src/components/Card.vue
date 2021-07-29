<template>
    <div class="bg-card">
      <div class="bg-card__noise"></div>
  </div>
</template>

<script>
export default {
  components: {},
};
</script>

<style lang="scss">
$main-color: linear-gradient(319.66deg, #20E4FF 19%, #8B30FF 75.54%);
$card-border: linear-gradient(90deg, rgba(73,73,73,1) 0%, rgba(0,0,0,0) 100%);
$card-bg: rgba(39, 39, 47, 0.4);

.bg-card {
  border-radius: 20px;
  background-color: $card-bg;
  position: absolute;
  overflow: hidden;
  border: 1px solid #494949;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: -1;
  backdrop-filter: blur(20px);

  &__noise {
    position: absolute;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    opacity: 0.4;
    background: url('../assets/Project/noise.png');
    background-repeat: repeat;
  }
}
</style>
