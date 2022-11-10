<template>
  <h6 class="selectable">{{ vault.name }}</h6>
</template>


<script>
import { Vault } from "../models/Vault";
import { vaultKeepsService } from "../services/VaultKeepsService";
import Pop from "../utils/Pop";

export default {
  props: {
    vault: { type: Vault, required: true },
  },
  setup(props) {
    return {

    }
  }
}
</script>


<style lang="scss" scoped>

</style>