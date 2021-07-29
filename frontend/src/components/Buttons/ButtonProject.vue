<template>
  <div class="button-project">
    <p>{{ textButton }}</p>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    textButton: String,
    width: String,
    height: String,
  },
  mounted() {
    let buttons = document.getElementsByClassName("button-project");
    for (let button in buttons) {
      if (
        buttons[button].style &&
        buttons[button].innerHTML === `<p>${this.textButton}</p>`
      ) {
        buttons[button].style.width = this.width;
        buttons[button].style.height = this.height;
      }
    }
    // console.log(buttons);
  },
};
</script>


<style lang="scss">
$bg-color: #060606;
$main-color: linear-gradient(319.66deg, #20e4ff 19%, #8b30ff 75.54%);
$card-bg: rgba(39, 39, 47, 0.4);

.button-project {
  display: flex;
  justify-content: center;
  align-items: center;
  border: solid 2px transparent;
  background: $main-color;
  background-origin: border-box;
  backdrop-filter: blur(20px);
  box-shadow: inset 2px 1000px 1px rgb(20, 20, 20, 1);
  transition: 0.3s;
  border-radius: 15px;
  p {
    transition: 0.3s;
    margin: 0;
  }
  &:hover {
    box-shadow: none;
    transition: 0.3s;
    background: $main-color;
    background-origin: border-box;
    cursor: pointer;
    p {
      transition: 0.3s;
      color: #000000 !important;
    }
  }
}
</style>