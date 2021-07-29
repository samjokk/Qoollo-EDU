<template>
  <div class="cards">
    <div id="card_first" class="card">
      <p>Придумай идею</p>
      <img alt="img" src="../../assets/Landing/Idea.png" />
      <span>1</span>
    </div>
    <div id="card_second" class="card">
      <p>Собери команду</p>
      <img alt="img" src="../../assets/Landing/Team.png" />
      <span>2</span>
    </div>
    <div id="card_third" class="card">
      <p>Разработай</p>
      <img alt="img" src="../../assets/Landing/Develop.png" />
      <span>3</span>
    </div>
    <div id="card_fourth" class="card">
      <p>Презентуй</p>
      <img alt="img" src="../../assets/Landing/Present.png" />
      <span>4</span>
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  props: {},
};
</script>

<style lang="scss">
.cards {
  margin-left: 495px;
  width: 700px;
  height: 700px;
  position: absolute;
  backface-visibility: hidden;
  animation: orbit 50s linear infinite 0s;
  .card {
    width: 160px;
    display: flex;
    flex-direction: column;
    position: absolute;
    transform-origin: center;
    animation: orbitBack 50s linear infinite 0s;
    p {
      font-family: Raleway;
      text-align: center;
      margin-bottom: 10px;
    }
    span {
      position: absolute;
      color: #000000 !important;
      top: 63px;
      right: 18px;
    }
  }
  #card_first {
    top: 0px;
    left: 270px;
  }
  #card_second {
    top: 219.5px;
    left: 0px;
  }
  #card_third {
    top: 439px;
    left: 270px;
  }
  #card_fourth {
    top: 219.5px;
    left: 540px;
  }
}

@keyframes orbit {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}

@keyframes orbitBack {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(-360deg);
  }
}
</style>
