<template>
  <div>{{ vault.name }}</div>
</template>


<script>
import { Vault } from "../models/Vault";

export default {
  props: {
    vault: { type: Vault, required: true },
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>