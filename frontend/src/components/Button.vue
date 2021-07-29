<template>
  <!--<div class="button__border-wrapper">-->
  <div class="button">
    {{ text }}
  </div>
  <!--</div>-->
</template>

<script>
export default {
  components: {},
  props: {
    text: String,
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

.button {
  padding: 5px 15px;
  transition: 0.3s;
  text-align: center;
  border: solid 3px transparent;
  background-origin: border-box;
  background-image: $main-color;
  border-radius: 10px;
  box-shadow: inset 2px 1000px 1px $bg-color;
  width: fit-content;
  p {
    transition: 0.3s;
    vertical-align: middle;
  }
  &:hover {
    box-shadow: none;
    transition: 0.3s;
    cursor: pointer;
    p {
      transition: 0.3s;
      color: #000000 !important;
    }
  }
}
</style>
