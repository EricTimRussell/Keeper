<template>
  <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${vault.img})` }">
    <div class="card-body">
    </div>
    <div class="card-footer outline d-flex text-shadow">
      <h3 class="selectable">
        {{ vault.name }}
      </h3>
    </div>
  </div>
</template>


<script>
import { Vault } from "../models/Vault";

export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.bgimg {
  height: 35vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}

.text-shadow {
  text-shadow: 1px 1px 5px #ff00e1, 0 0 1em #8888f7, 0 0 0.2em #ffffff;
}
</style>