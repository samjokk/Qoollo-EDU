<template>
  <div class="button-landing">
    <p>{{ textButton }}</p>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    textButton: String,
  },
};
</script>


<style lang="scss">
$bg-color: #060606;
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$card-bg: rgba(39, 39, 47, 0.4);

.button-landing {
  padding: 23px 0px 0px 0px;
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