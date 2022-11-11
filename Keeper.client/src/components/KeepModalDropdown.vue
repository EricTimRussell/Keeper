<template>
  <h6 class="selectable">{{ vaults.name }}</h6>
</template>


<script>
import { Vault } from "../models/Vault";


export default {
  props: {
    vaults: { type: Vault, required: true },
  },
  setup(props) {
    return {

    }
  }
}
</script>


<style lang="scss" scoped>

</style>