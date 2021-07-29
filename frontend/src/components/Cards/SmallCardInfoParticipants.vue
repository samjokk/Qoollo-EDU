<template>
  <div class="small-card-info-participants">
    <p class="text-info">{{ textCard }}</p>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    textCard: String,
  },
};
</script>

<style lang="scss">
$card-bg: rgba(39, 39, 47, 0.4);
.small-card-info-participants {
  width: 380px;
  height: 90px;
  display: flex;
  justify-content: center;
  border-radius: 25px;
  background-color: $card-bg;
  backdrop-filter: blur(20px);
  background: url("../../assets/Landing/SmallCardBackgroundParticipants.png");
  .text-info {
    font-family: Raleway;
    font-size: 36px;
    align-self: center;
  }
}
</style>
