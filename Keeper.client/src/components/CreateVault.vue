<template>
  <form @submit.prevent="createVault()">
    <div class="form-floating mb-3">
      <input v-model="editable.name" type="text" class="form-control" id="vaultName" placeholder="Title">
      <label for="floatingInput">Title...</label>
    </div>
    <div class="form-floating">
      <input v-model="editable.img" type="url" class="form-control" id="vaultImg" placeholder="Image">
      <label for="vaultImage">Image Url..</label>
    </div>
    <div class="input-group my-2">
      <input v-model="editable.isPrivate" class="form-check-input selectable" type="checkbox"
        aria-label="Checkbox for following text input">
      <p class="px-1">Private?</p>
    </div>
    <div class="my-3">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Submit</button>
    </div>
  </form>
</template>


<script>
import { Modal } from "bootstrap";
import { ref } from "vue";
import { vaultsService } from "../services/VaultsService";
import Pop from "../utils/Pop";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          const formData = editable.value
          await vaultsService.createVault(formData)
          Modal.getOrCreateInstance('#vault').hide()
        } catch (error) {
          Pop.error(error, "Creating Vault")
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>